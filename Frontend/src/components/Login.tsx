import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect, useRef, MutableRefObject } from 'react';
import { AdminData, AdminDataDTO } from '../models/Admin';

interface LoginProps {
    setIsLoggedIn: (value: boolean) => void;
    //setAdminData: (value: AdminData) => void;
    adminDataDTORef: MutableRefObject<AdminDataDTO>;
    loginFormDataRef: MutableRefObject<{username: string}>
    //setSavedLoginFormData: (value: AdminData) => void;
    //getSavedLoginFormData: (value: AdminData) => void;
    //initAdminData: (value: AdminData) => void;
    // setFirstName: (value: string) => void;
    // setLastName: (value: string) => void;
    // setEmail: (value: string) => void;
}
const Login: React.FC<LoginProps> = ({adminDataDTORef, loginFormDataRef, setIsLoggedIn}) => {

    const [formData, setFormData] = useState(
        {
            username: loginFormDataRef.current.username,
            password: '', //this should be safe
        }
    )
    // const [localAdminDataDTO, setLocalAdminDataDTO] = useState(
    //     {
    //         adminId: 0,
    //         username: '',
    //         //password: '',
    //         email: ''
    //     }
    // )
    const [statusMessage, setStatusMessage] = useState("");
    
    // on submit - using local
    // const updateAdminDataDTO= () => adminDataDTORef.current = {
    //     ...adminDataDTORef.current,
    //     ...localAdminDataDTO // Merge formData into adminDataRef
    // };

    //on submit
    const updateAdminDataDTO= (localAdminDataDTO: AdminDataDTO ) => adminDataDTORef.current = {
        ...adminDataDTORef.current,
        ...localAdminDataDTO // Merge formData into adminDataRef
    };

    //on change
    //const updateLoginFormDataRef= () => {loginFormDataRef.current.username = formData.username};
    
    useEffect(() => {
        loginFormDataRef.current.username = formData.username;
    }, [formData.username, loginFormDataRef]);
    
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        try {
            const loginResponse = await fetch("/Login", { //returns a view along with response, which isnt used so its fine
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(formData) //send username and password
            });

            if (loginResponse.ok) { //we could have Login above return AdminData itself, but it might be better to seperate concerns
                //primarily to get adminId
                const getAdminDataResponse = await fetch("/Login/AdminData", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(formData) //send username and password
                    //this does not update the local data, but uses it to send. the try above only continues if its succesful
                })
                setIsLoggedIn(true);
                const responseAdminData: AdminDataDTO = await getAdminDataResponse.json()
                // const localAdminDataDTO: AdminDataDTO = {
                //     adminId: responseAdminData.adminId,
                //     username: responseAdminData.username,
                //     email: responseAdminData.email
                // }
                updateAdminDataDTO(responseAdminData) //assumes response has same fields
                
                const data = await loginResponse.json();
                setStatusMessage("succesful login")
                console.log("succesful login");
            } else {
                setStatusMessage("failed login")
                console.log("failed login");
            }

        } catch (error) {
            console.error();
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setFormData({
            ...formData,
            [id]: value,
        });
        //updateLoginFormDataRef(); // now done through UseEffect() to always keep them synced, this old way didnt guarantee that
    };
    
    return (
        <div>
            <form onSubmit={handleLogin}>
            <label htmlFor='username'>username</label>
                    <input
                    type="text"
                    id="username"
                    value={ formData.username }
                    onChange={ handleChange }
                    required
                    /> <br />
                {/* <label htmlFor="lastName">lastName</label>
                <input
                    type="text"
                    id="lastName"
                    value={customer.lastName}
                    onChange={handleChange}
                    required
                    /> <br />
                <label htmlFor="email">email</label> */}
            <label htmlFor='password'>password</label>
                <input
                    type="text"
                    id="password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                    /> <br />
                <button type="submit">Login</button>
                <div>aa {statusMessage} bb</div> {/* Display the value of data */}
            </form>
        </div>
    )
};

export default Login;

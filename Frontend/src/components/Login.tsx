import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect, useRef } from 'react';
import { AdminData } from '../models/Admin';

interface LoginProps {
    setIsLoggedIn: (value: boolean) => void;
    setAdminData: (value: AdminData) => void;
    setSavedLoginFormData: (value: AdminData) => void;
    getSavedLoginFormData: (value: AdminData) => void;
    //initAdminData: (value: AdminData) => void;
    // setFirstName: (value: string) => void;
    // setLastName: (value: string) => void;
    // setEmail: (value: string) => void;
}
const Login: React.FC<LoginProps> = ({setIsLoggedIn, setAdminData, setSavedLoginFormData, getSavedLoginFormData}) => {

    const [formData, setFormData] = useState(
        {
            adminId: 0,
            username: '',
            password: '', //this should be safe
            email: ''
        }
    )
    const [localAdminData, setLocalAdminData] = useState(
        {
            adminId: 0,
            username: '',
            password: '', //this should be safe
            email: ''
        }
    )
    const [statusMessage, setStatusMessage] = useState("");
    
    
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        try {
            const loginResponse = await fetch("/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                }
                //body: JSON.stringify(formData) //sending here
            });

            if (loginResponse.ok) {
                const getAdminDataResponse = await fetch("/Login/AdminData", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(localAdminData) //sending here 
                    //this does not update the local data, but uses it to send. the try above only continues if its succesful
                })
                setIsLoggedIn(true);
                setAdminData(formData)
                // setFirstName(customer.firstName)
                // setLastName(customer.lastName)
                // setEmail(customer.email)
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

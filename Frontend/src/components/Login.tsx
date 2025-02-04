import { BrowserRouter as Router, Route, Routes, Link, redirect } from 'react-router-dom';
import React, { useState, useEffect, useRef, MutableRefObject } from 'react';
import { AdminData, AdminDataDTO } from '../models/Admin';
import { useNavigate } from "react-router-dom";

interface LoginProps {
    setIsLoggedIn: (value: boolean) => void;
    adminDataDTORef: MutableRefObject<AdminDataDTO>; //"Data Transfer Object" (excludes password)
    loginFormDataRef: MutableRefObject<{username: string}>
}
const Login: React.FC<LoginProps> = ({adminDataDTORef, loginFormDataRef, setIsLoggedIn}) => {
    localStorage
    const navigate = useNavigate()
    const [formData, setFormData] = useState(
        {
            username: loginFormDataRef.current.username,
            password: '', //this should be safe here. we dont store this in loginFormDataRef
        }
    )
    const [statusMessage, setStatusMessage] = useState("");
    
    //on submit - update AdminData in App.tsx using fetched DTO, which includes everything but the password
    const updateAdminDataDTO= (newAdminDataDTO: AdminDataDTO ) => adminDataDTORef.current = {
        ...adminDataDTORef.current,
        ...newAdminDataDTO 
    };

    //on change - keep track of filled in username in App.tsx, to reload from when re-entering login page
    useEffect(() => {
        loginFormDataRef.current.username = formData.username;
    }, [formData.username, loginFormDataRef]);
    
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        try { 
            const loginResponse = await fetch("/Login", { //returns a view along with response, which isnt used so its fine
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                },
                body: new URLSearchParams(formData).toString() //send username and password
            });

            if (loginResponse.ok) { 
                //we could have Login above return AdminData itself, but it might be better to seperate concerns
                //primarily to get adminId
                const getAdminDataResponse = await fetch("/Login/AdminData", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded",
                    },
                    body: new URLSearchParams(formData).toString() //send username and password
                })
                try{
                    const text = await getAdminDataResponse.text()
                    
                    if(getAdminDataResponse.ok)
                        {
                            if(!text)
                            {
                                setStatusMessage("there was no getAdminDataResponse");
                            }
                            else
                            {
                                const responseAdminData: AdminDataDTO = await JSON.parse(text)
                                updateAdminDataDTO(responseAdminData) //assumes response has same fields
                                setIsLoggedIn(true);
                                setStatusMessage("succesful login")
                                console.log("succesful login");
                                navigate("/AdminDashboard")
                            }
                        }
                    else {
                        if(!text)
                            {
                                setStatusMessage("there was no getAdminDataResponse");
                            }
                        else {
                            setStatusMessage("failed login1" + text)
                        }
                    }
                }
                catch (e)
                {
                    //var result = e.message; // error under useUnknownInCatchVariables 
                    if (typeof e === "string") {
                        setStatusMessage("error1:" + e.toUpperCase())
                    } else if (e instanceof Error) {
                        setStatusMessage("error2:" + e.message + " : " + e.stack)
                    }
                }
            } else {
                setStatusMessage("failed login2: " + loginResponse.status + " - " + loginResponse.statusText + "-")
                console.log("failed login");
            }
        } catch (e)
        {
            //var result = e.message; // error under useUnknownInCatchVariables 
            if (typeof e === "string") {
                setStatusMessage("error3:" + e.toUpperCase())
            } else if (e instanceof Error) {
                setStatusMessage("error4:" + e.message + " : " + e.stack)
            }
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
            <label htmlFor='password'>password</label>
                <input
                    type="text"
                    id="password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                    /> <br />
                <button type="submit">Login</button>
                <div>{statusMessage}</div>
            </form>
        </div>
    )
};

export default Login;

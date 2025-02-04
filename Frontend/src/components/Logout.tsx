import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";


interface LogoutProps {
    setIsLoggedIn: (value: boolean) => void;
};

const Logout: React.FC<LogoutProps> = ({setIsLoggedIn}) => {
    const navigate = useNavigate()

    const handleLogOut = async () => {
        const logoutResponse = await fetch("/Login/logout", {
            method: "GET"
        });
        if(logoutResponse.ok)
        {
            setIsLoggedIn(false);
            alert("You have been logged out.");
            navigate("/Login")
        }
    }

    return (
        <div>
            <h1>Are you sure?</h1>
            <button onClick={handleLogOut}>yes</button>
            <Link to="/Home" className="button-link">No</Link>
        </div>
    );
};

export default Logout;
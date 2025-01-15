import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

interface LogoutProps {
    setIsLoggedIn: (value: boolean) => void;
};

const Logout: React.FC<LogoutProps> = ({setIsLoggedIn}) => {

    const handleLogOut = () => {
        setIsLoggedIn(false);
        alert("You have been logged out.");
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
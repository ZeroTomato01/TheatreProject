import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

const Account: React.FC = () => {
    return (
        <div>
            <h1>Account Information</h1>
            <ul>
                <li>Name</li>
                <li>Email</li>
                <li>Password</li>
                <li>your shows also a list probably</li>
            </ul>
        </div>
    )
}

export default Account;
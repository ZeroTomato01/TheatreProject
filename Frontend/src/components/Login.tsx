import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

const Login: React.FC = () => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    
    const handleLogin = () => {};
    
    return (
        <div>
            <form onSubmit={handleLogin}>
                <label htmlFor='username'>Username</label>
            </form>
            <br />
            <button>submit</button>
        </div>
    )
};

export default Login;

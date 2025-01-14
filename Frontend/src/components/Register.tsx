import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

const Register: React.FC = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const handleRegister = () => {};
    return (
        <form onSubmit={handleRegister}>
            <label htmlFor='username'>Username</label>

        </form>
    );
};

export default Register;
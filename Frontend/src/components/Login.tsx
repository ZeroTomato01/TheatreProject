import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

interface LoginProps {
    setIsLoggedIn: (value: boolean) => void;
}
const Login: React.FC<LoginProps> = ({setIsLoggedIn}) => {

    const [customer, setCustomer] = useState(
        {
            customerId: 0,
            firstName: '',
            lastName: '',
            email: '',
        }
    )
    
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        try {
            const response = await fetch("/Customer/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(customer) //sending here
            });

            if (response.ok) {
                setIsLoggedIn(true);
                const data = await response.json();
                console.log("succes");
            } else console.log("failure");
        } catch (error) {
            console.error();
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        setCustomer({
            ...customer,
            [id]: value,
        });
    };
    
    return (
        <div>
            <form onSubmit={handleLogin}>
            <label htmlFor='firstName'>firstName</label>
                    <input
                    type="text"
                    id="firstName"
                    value={ customer.firstName }
                    onChange={ handleChange }
                    required
                    /> <br />
                <label htmlFor="lastName">lastName</label>
                <input
                    type="text"
                    id="lastName"
                    value={customer.lastName}
                    onChange={handleChange}
                    required
                    /> <br />
                <label htmlFor="email">email</label>
                <input
                    type="text"
                    id="email"
                    value={customer.email}
                    onChange={handleChange}
                    required
                    /> <br />
                <button type="submit">Register</button>
            </form>
        </div>
    )
};

export default Login;

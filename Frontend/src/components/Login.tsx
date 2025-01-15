import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

interface LoginProps {
    setIsLoggedIn: (value: boolean) => void;
    setFirstName: (value: string) => void;
    setLastName: (value: string) => void;
    setEmail: (value: string) => void;
}
const Login: React.FC<LoginProps> = ({setIsLoggedIn, setFirstName, setLastName, setEmail}) => {

    const [customer, setCustomer] = useState(
        {
            customerId: 0,
            firstName: '',
            lastName: '',
            email: '',
        }
    )
    const [data, setData] = useState(""); // Use useState for data
    
    
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
                setFirstName(customer.firstName)
                setLastName(customer.lastName)
                setEmail(customer.email)
                const data = await response.json();
                setData("succesful login")
                console.log("succesful login");
            } else {
                setData("failed login")
                console.log("failed login");
            }

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
                <button type="submit">Login</button>
                <div>aa {data} bb</div> {/* Display the value of data */}
            </form>
        </div>
    )
};

export default Login;

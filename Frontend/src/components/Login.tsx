import React, { useState, useEffect, MutableRefObject } from 'react';
import { useNavigate } from "react-router-dom";
import { AdminDataDTO } from '../models/Admin';
import { useAdmin } from './AdminContext';
import { Container, Form, Button, Alert } from 'react-bootstrap';

interface LoginProps {
    adminDataDTORef: MutableRefObject<AdminDataDTO>; //"Data Transfer Object" (excludes password)
    loginFormDataRef: MutableRefObject<{ username: string }>
}

const Login: React.FC<LoginProps> = ({ adminDataDTORef, loginFormDataRef }) => {
    const navigate = useNavigate();
    const { setIsLoggedIn } = useAdmin();
    const [formData, setFormData] = useState({
        username: loginFormDataRef.current.username,
        password: '', //this should be safe here. we dont store this in loginFormDataRef
    });
    const [statusMessage, setStatusMessage] = useState("");

    //on submit - update AdminData in App.tsx using fetched DTO, which includes everything but the password
    const updateAdminDataDTO = (newAdminDataDTO: AdminDataDTO) => adminDataDTORef.current = {
        ...adminDataDTORef.current,
        ...newAdminDataDTO
    };

    //on change - keep track of filled in username in App.tsx, to reload from when re-entering login page
    useEffect(() => {
        loginFormDataRef.current.username = formData.username;
    }, [formData.username, loginFormDataRef]);

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            const response = await fetch("/Login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                setStatusMessage("Login failed. Please check your credentials.");
            } else {
                const data: AdminDataDTO = await response.json();
                updateAdminDataDTO(data);
                setIsLoggedIn(true);
                navigate("/AdminDashboard");
            }
        } catch (error) {
            console.error("Login failed:", error);
            setStatusMessage("An error occurred. Please try again.");
        }
    };

    return (
        <Container className="mt-5">
            <h2 className="text-center mb-4">Login</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group controlId="formUsername">
                    <Form.Label>Username</Form.Label>
                    <Form.Control
                        type="text"
                        value={formData.username}
                        onChange={(e) => setFormData({ ...formData, username: e.target.value })}
                        required
                    />
                </Form.Group>
                <Form.Group controlId="formPassword" className="mt-3">
                    <Form.Label>Password</Form.Label>
                    <Form.Control
                        type="password"
                        value={formData.password}
                        onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                        required
                    />
                </Form.Group>
                <Button variant="primary" type="submit" className="mt-4 w-10">
                    Login
                </Button>
            </Form>
            {statusMessage && <Alert variant="danger" className="mt-3">{statusMessage}</Alert>}
        </Container>
    );
};

export default Login;
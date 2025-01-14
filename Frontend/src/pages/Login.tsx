
import { JSX } from "react"
import React, { useState } from "react";
import { Map } from "immutable"

const Login: React.FC = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
  
    const handleLogin = async (e: React.FormEvent) => {
      e.preventDefault();
  
      const formData = new FormData();
      formData.append("username", username);
      formData.append("password", password);
  
      try {
        const response = await fetch("/api/LoginAction", {
          method: "POST",
          body: formData,
          credentials: "include", // Ensures cookies (session) are sent
        });
  
        if (response.redirected) {
          // Handle the redirect
          window.location.href = response.url;
        } else {
          const data = await response.text();
          console.log("Login failed:", data);
        }
      } catch (error) {
        console.error("Error during login:", error);
      }
    };
  
    return (
      <form onSubmit={handleLogin}>
        <div>
          <label>
            Username:
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </label>
        </div>
        <div>
          <label>
            Password:
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </label>
        </div>
        <button type="submit">Login</button>
      </form>
    );
  };
  
  export default Login;
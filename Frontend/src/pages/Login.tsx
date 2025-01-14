import { JSX } from "react";
import React, { useState } from "react";

const Login: React.FC = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [data, setData] = useState(""); // Use useState for data

    const handleLogin = async (e: React.FormEvent) => {
      e.preventDefault();
  
      const formData = new FormData();
      formData.append("username", username);
      formData.append("password", password);
  
      try {
        const response = await fetch("/Login/LoginAction", {
          method: "POST",
          body: formData,
          credentials: "include", // Ensures cookies (session) are sent
        });
  
        if (response.redirected) {
          // Handle the redirect
          window.location.href = response.url;
          setData("Redirected"); // Set the state to indicate a redirect
        } else {
          setData("Login failed"); // Set the state when login fails
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
        <div>aa {data} bb</div> {/* Display the value of data */}
      </form>
    );
};

export default Login;

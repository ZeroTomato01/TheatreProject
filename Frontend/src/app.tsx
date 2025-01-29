import React, { useState, useRef } from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Shows from './components/Shows';
import MenuBar from './components/MenuBar';
import Login from './components/Login';
import Logout from './components/Logout';
import Privacy from './components/Privacy';
import Home from './components/Home'
import AdminDashboard from './components/AdminDashboard';
import { AdminDataDTO } from './models/Admin';


const App: React.FC = () => {
  const adminDataDTORef = useRef<AdminDataDTO>({
    adminId: 0,
    username: '',
    //password: '', 
    email: ''
  });
  const loginFormDataRef = useRef({username: ''});
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  //const [firstName, setFirstName] = useState("");
 // const [lastName, setLastName] = useState("");
  //const [email, setEmail] = useState("");
  return (
    <Router>
      <MenuBar isLoggedIn={isLoggedIn}/>
  
      <Routes>
        <Route path="/home" element={<Home />} />
        <Route path="/shows" element={<Shows/>} />
        <Route path="/login" element={<Login 
        adminDataDTORef={adminDataDTORef}
        loginFormDataRef={loginFormDataRef}
        setIsLoggedIn={setIsLoggedIn}
        />} />
        <Route path="/adminDashboard" element={<AdminDashboard />} />
        <Route path="/logout" element={<Logout setIsLoggedIn={setIsLoggedIn} />} />
        <Route path="/privacy" element={<Privacy />} />
        <Route path="*" element={<Home />} /> 
      </Routes>
    </Router>
  )};
  
  export default App;
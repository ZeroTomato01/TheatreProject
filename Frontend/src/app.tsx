import React, { useState, useRef } from 'react'
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Shows from './components/Shows';
import MenuBar from './components/MenuBar';
import Login from './components/Login';
import Logout from './components/Logout';
import Privacy from './components/Privacy';
import Home from './components/Home'
import AdminDashboard from './components/AdminDashboard';
import NotFound from './components/NotFound';
import { AdminDataDTO } from './models/Admin';
import Reserve from './components/Reserve';


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
        <Route path="/" element={<Navigate to="/Home" />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/Shows" element={<Shows/>} />
        <Route path="/Login" element={<Login 
        adminDataDTORef={adminDataDTORef}
        loginFormDataRef={loginFormDataRef}
        setIsLoggedIn={setIsLoggedIn}
        />} />
        <Route path="/AdminDashboard" element={<AdminDashboard />} />
        <Route path="/Logout" element={<Logout setIsLoggedIn={setIsLoggedIn} />} />
        <Route path="/Privacy" element={<Privacy />} />
        <Route path="/Reserve/:showDateId" element={<Reserve />} />
        <Route path="*" element={<NotFound />} /> 
      </Routes>
    </Router>
  )};
  
  export default App;
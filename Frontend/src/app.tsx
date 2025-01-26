import React, { useState } from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Shows from './components/Shows';
import MenuBar from './components/MenuBar';
import Login from './components/Login';
import Logout from './components/Logout';
import Register from './components/Register';
import Privacy from './components/Privacy';
import Account from './components/Account';
import Home from './components/Home'

import { AdminDataWrapper } from './models/Admin';
//import { CustomerData } from './models/customer';

const App: React.FC = () => {
  const [adminData, setAdminData] = useState<AdminDataWrapper | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  //const [firstName, setFirstName] = useState("");
 // const [lastName, setLastName] = useState("");
  //const [email, setEmail] = useState("");
  return (
    <Router>
      <MenuBar isLoggedIn={isLoggedIn}/>
  
      <Routes>
        <Route path="/Home" element={<Home />} />
        <Route path="/Shows" element={<Shows />} />
        <Route path="/Login" element={<Login 
        setIsLoggedIn={setIsLoggedIn}
        setAdminData={setAdminData}
        // setFirstName={setFirstName}
        // setLastName={setLastName}
        // setEmail={setEmail}
        />} />
        <Route path="/Logout" element={<Logout setIsLoggedIn={setIsLoggedIn} />} />
        <Route path="/Register" element={<Register setIsLoggedIn={setIsLoggedIn} />} />
        <Route path="/Privacy" element={<Privacy />} />
        {/* <Route path="/Account" element={<Account 
              firstName={firstName}
              lastName={lastName}
              email={email}/>} /> */}
        
        
      </Routes>
    </Router>
  )};
  
  export default App;
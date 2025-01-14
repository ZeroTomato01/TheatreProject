import React from 'react'
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Shows from './components/Shows';
import MenuBar from './components/MenuBar';
import Login from './components/Login';
import Logout from './components/Logout';
import Register from './components/Register';
import Privacy from './components/Privacy';
import Account from './components/Account';


const App: React.FC = () => (
    <Router>
      <MenuBar isLoggedIn={false}/>
  
      <Routes>
        <Route path="/Shows" element={<Shows />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/Logout" element={<Logout />} />
        <Route path="/Register" element={<Register />} />
        <Route path="/Privacy" element={<Privacy />} />
        <Route path="/Account" element={<Account />} />
        
      </Routes>
    </Router>
  );
  
  export default App;
import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './styles.css';

interface MenuBarProps {
    isLoggedIn: boolean
};

const MenuBar: React.FC<MenuBarProps> = ({isLoggedIn}) => {
    return (
      <nav className = 'menu-bar'>
        <h1>BerserkerCinema</h1>
        <ul>
          <li><Link to='/Home'>Home</Link></li>
          <li><Link to='/Shows'>Shows</Link></li>
          <li><Link to='/Privacy'>Privacy</Link></li>
          {isLoggedIn ? (
            <>
              <li><Link to='/Profile'>Profile</Link></li>
              <li><Link to='/Logout'>Logout</Link></li>
            </>
          ) : (
            <>
            <li><Link to='/Register'>Register</Link></li>
            <li><Link to='/Login'>Login</Link></li>
            </>
          )}
        </ul>
      </nav>
    );
  };
  
  export default MenuBar;
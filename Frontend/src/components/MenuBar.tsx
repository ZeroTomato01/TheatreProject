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
          <li><Link to='/home'>Home</Link></li>
          <li><Link to='/shows'>Shows</Link></li>
          <li><Link to='/privacy'>Privacy</Link></li>
          {isLoggedIn ? (<>
              <li><Link to='/account'>Profile</Link></li>
              <li><Link to='/adminDashboard'>AdminDashboard</Link></li>
              <li><Link to='/logout'>Logout</Link></li>
            </>) 
            : 
            (<>
            <li><Link to='/login'>Login</Link></li>
            </>)
          }
        </ul>
      </nav>
    );
  };
  
  export default MenuBar;
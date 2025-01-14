import React from 'react';
import './menubar.css';

interface MenuBarProps {
    isLoggedIn: boolean
};

const MenuBar: React.FC<MenuBarProps> = ({ isLoggedIn}) => {
    return (
      <nav className = 'menu-bar'>
        <h1>BerserkerCinema</h1>
        <ul>
          <li>Home</li>
          <li>Films</li>
          <li>Privacy</li>
          {isLoggedIn ? (
            <>
              <li>Profile</li>
              <li>Logout</li>
            </>
          ) : (
            <>
            <li>Register</li>
            <li>Login</li>
            </>
          )}
        </ul>
      </nav>
    );
  };
  
  export default MenuBar;
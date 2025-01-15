import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

interface AccountProps {
    firstName: string;
    lastName: string;
    email: string;
  }
  
  const Account: React.FC<AccountProps> = ({ firstName, lastName, email }) => {
    return (
      <div>
        <h1>Account Information</h1>
        <ul>
          <li>Name: {firstName} {lastName}</li>
          <li>Email: {email}</li>
          <li>Password: (Hidden for security)</li>
          <li>Your shows: (List goes here)</li>
        </ul>
      </div>
    );
  };
  
  export default Account;
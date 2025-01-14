import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';

const Logout: React.FC = () => {
    return (
        <div>
            <h1>Are you sure?</h1>
            <button>yes</button>
            <button>no</button>
        </div>
    );
};

export default Logout;
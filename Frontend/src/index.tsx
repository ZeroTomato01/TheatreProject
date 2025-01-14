// import * as React from "react";
// import { createRoot } from 'react-dom/client';
//import Home from "./pages/Home";

// createRoot(document.getElementById('root')!)
//     .render(<React.StrictMode>
//         <Home />
//     </React.StrictMode>)


//from Ch3Unit1
import React from 'react';
import ReactDOM from 'react-dom/client';
import { App } from './app';
import { Home } from './pages/Home'
import { BrowserRouter } from "react-router-dom";


const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <BrowserRouter>
    <Home />
  </BrowserRouter>
);

// export const main = () =>
// {
//   const root = ReactDOM.createRoot(
//     document.getElementById('root') as HTMLElement
//   )
//   root.render(

//   <Home />
  
    
//   )
// }



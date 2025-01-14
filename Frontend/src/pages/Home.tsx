//From Ch3Unit1

import React from 'react';
import { JSX } from "react";
import { Person } from '../app.state';
import { HomeState, initHomeState } from "./Home.state"
import { RegistrationForm } from './Registration';
import { Link, Route, Routes } from "react-router-dom";
import Login from "./Login"

export interface HomeProps{

}

export class Home extends React.Component<{}, HomeState> { //Home is a COMPONENT, just like RegistrationForm
    constructor(props: {}) {
        super(props)
        this.state = initHomeState
    }
    
    render(): JSX.Element {
    return (

        <div>
        <Routes>
            {/* Default Home Page */}
            <Route
            path="/"
            element={
                <div>
                Welcome to the person management system!
                <div>
                    <Link to="/registration">
                    Registration
                    </Link>
                </div>
                <div>
                    <Link to="/Login">
                    <button>Login</button>
                    </Link>
                </div>
                <div>
                    <Link to="/account">
                    <button>Account</button>
                    </Link>
                </div>
                <div>
                    <Link to="/shows">
                    <button>Shows</button>
                    </Link>
                </div>
                </div>
            }
            />
            {/* Registration Page */}
            <Route
            path="/registration"
            element={
                <RegistrationForm
                insertPerson={(person: Person) => {
                    console.log("Insert Person:", person);
                }}
                backToHome={() => window.history.back()}
                registrationState={this.state.registrationState} // Pass actual state here
                updateName={(name: string) => console.log("Update Name:", name)}
                updateLastName={(lastName: string) =>
                    console.log("Update Last Name:", lastName)
                }
                updateAge={(age: number) => console.log("Update Age:", age)}
                updateMessage={(message: string) =>
                    console.log("Update Message:", message)
                }
                />
            }
            />
            
            {/* Login Page */}
            <Route
            path="/Login"
            element={
                <Login/>
            }
            />
        </Routes>
        </div>)
        }
      }





// export class Home extends React.Component<{}, HomeState> { //Home is a COMPONENT, just like RegistrationForm
//     constructor(props: {}) {
//         super(props)
//         this.state = initHomeState
//     }
    
//     render(): JSX.Element {
//         switch (this.state.view) {
//           case "home":
//             return (
//               <div>
//                 <div>
//                   Welcome to the person management system!
//                   <div>
//                     <button
//                       onClick={_ => this.setState(this.state.setViewState("registration"))}
//                     >
//                       Registration
//                     </button>
//                   </div>
//                   <div>
//                     <button
//                       onClick={_ => this.setState(this.state.setViewState("overview"))}
//                     >
//                       Overview
//                     </button>
//                   </div>
//                 </div>
//               </div>
//             )

//             case "registration":
//                 return (
//                   <RegistrationForm 
//                     insertPerson={(person: Person) => this.setState(this.state.insertPerson(person))}
//                     backToHome={() => this.setState(this.state.setViewState("home"))}
//                     registrationState={this.state.registrationState} //added in lifting
//                     updateName={(name: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateName(name)))} //added in lifting
//                     updateLastName={(lastName: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateLastName(lastName)))} //added in lifting
//                     updateAge={(age: number) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateAge(age)))} //added in lifting
//                         //exercise 2
//                     updateMessage={(message: string) => this.setState(this.state.updateRegistrationState(this.state.registrationState.updateMessage(message)))}
//                   /> 
//             )
//             case "overview":
//             return (
//                 <Overview
//                 people={this.state.storage}
//                 backToHome={() => this.setState(this.state.setViewState("home"))}
//                 />
//             )

//             default: return <></>
//         }
//       }
//     }
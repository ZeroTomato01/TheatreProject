import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import { AdminDataWrapper } from '../models/Admin';

  
  const Account: React.FC<AdminDataWrapper> = ({ adminData }) => {
    const [reservations, setReservations] = useState<any[]>([]); // Store a list of shows
    const [message, setMessage] = useState<string>(""); // Store a list of shows


    //the current implementation isn't correct, we need to change this to show all reservations with filter options
    const fetchReservations = async () => {
      try {

        const response1 = await fetch("/Customer/Find?email={email}", {
          method: "GET",
          headers: {
            Accept: "application/json",
          },
        });
        const id = response1;
        const response2 = await fetch("/Reservation?id=${id}", {
          method: "GET",
          headers: {
            Accept: "application/json",
          },
        });
  
        if (response2.ok) {
          const data = await response2.json();
          //setReservations(data.$values); // The shows are in the "$values" array
          setMessage("Your Reservations:");
        } else {
          setMessage(`We couldn't fetch your reservations :( ${response2.statusText} ${response2.status}`);
        }
      } catch (error) {
        console.error("Error fetching reservations:", error);
      }
    };
  
    useEffect(() => {
      fetchReservations(); // Fetch shows when the component mounts
    }, []);

    return (
      <div>
        <h1>Account Information</h1>
        <ul>
          <li>username: {adminData.username}</li>
          <li>Email: {adminData.email}</li>
          <li>Password: ()</li>
          <li>Your Reservations:
            {message && <p>{message}</p>}
            <ul>
              {reservations.length > 0 ? (
                reservations.map((reservation, index) => (
                  <li key={index}>
                    <p>Reservation ID: {reservation.reservationId}</p>
                    <p>Amount of Tickets: {reservation.amountOfTickets}</p>
                    <p>Used: {reservation.used ? "Yes" : "No"}</p>
                    <p>Theatre Show Date: {reservation.theatreShowDate?.dateAndTime}</p>
                  </li>
                ))
              ) : (
                <li>No reservations found</li>
              )}
            </ul>
          </li>
        </ul>
      </div>
    );
  }
  
  export default Account;
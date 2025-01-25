import React, { useState, useEffect } from 'react';


interface ReserveProps {
    CustomerEmail: string
    ShowId: number
}

const Reserve: React.FC<ReserveProps> = ({CustomerEmail, ShowId}) => {
   
    const [message, setMessage] = useState<string>(""); // Store a list of shows

    const HandleReserve = async () => {

        const response1 = await fetch("/Customer/Find?email={email}", {
            method: "GET",
            headers: {
              Accept: "application/json",
            },
          });

        const customerId = response1;

        const response2 = await fetch("/Customer", {
            method: "GET",
            headers: {
              Accept: "application/json",
            },
          });

        const customer = response2

        const reservation = {
            TheatreShowDate: {
              theatreShowId: ShowId, // Use the ShowId passed in as a prop
            },
            AmountOfTickets: 1,
            Used: false,
            Customer: customer
          };

        try {
            const response = await fetch("/Reservation", {
                method: "POST",
                headers: {
                  "Content-Type": "application/json",
                },
                body: JSON.stringify(reservation),
              });
    
          if (response.ok) {
            const data = await response.json();
            //setShows(data.$values); // The shows are in the "$values" array
            setMessage("Our available shows:");
          } else {
            setMessage(`We couldn't fetch the shows :( ${response.statusText} ${response.status}`);
          }
        } catch (error) {
          console.error("Error fetching shows:", error);
        }
      };
    
      useEffect(() => {
        HandleReserve(); // Fetch shows when the component mounts
      }, []);

    return (
        <div> reserved?</div>
  )
}

export default Reserve
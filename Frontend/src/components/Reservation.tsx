import React, { useState, useEffect } from 'react';

interface ReservationProps {
    showID: number
}

const Reservation: React.FC<ReservationProps> = ({showID}) => {
  const [show, setShow] = useState<any | null>(null); // Store a list of shows
  const [message, setMessage] = useState<string>(""); // Store a list of shows

  const fetchShow = async () => {
    try {
      const response = await fetch(`/TheatreShow/${showID}`, {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });

      if (response.ok) {
        const data = await response.json();
        setShow(data);
        setMessage("Total seats:");
      } else {
        setMessage(`We couldn't fetch the show :( ${response.statusText} ${response.status}`);
      }
    } catch (error) {
      console.error("Error fetching show:", error);
    }
  };

  useEffect(() => {
    fetchShow(); // Fetch shows when the component mounts
  }, [showID]);

    return (
        <div>
          <h1>Shows</h1>
          <div>{message}</div>
          <ul>
            {show ? (
              <li>
                <strong>{show.title}</strong>
                <p>{show.description}</p>
                <p>
                  <strong>Price:</strong> ${show.price}
                </p>
                <p>
                  <strong>Venue:</strong> {show.venue?.name || "No venue specified"}
                </p>
                <ul>
                  {show.theatreShowDates?.$values.map((showDate: any, idx: number) => (
                    <li key={idx}>
                      <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                    </li>
                  )) || <li>No show dates available</li>}
                </ul>
              </li>
            ) : (
              <li>Loading show...</li>
            )}
          </ul>
        </div>
      );
}
export default Reservation;

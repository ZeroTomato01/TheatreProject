import React, { useState, useEffect } from 'react';

const Shows: React.FC = () => {
  const [shows, setShows] = useState<any[]>([]); // Store a list of shows
  const [message, setMessage] = useState<string>(""); // Store a list of shows

  const fetchShows = async () => {
    try {
      const response = await fetch("/TheatreShow/get", {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });

      if (response.ok) {
        const data = await response.json();
        setShows(data.$values); // The shows are in the "$values" array
        setMessage("Our available shows:");
      } else {
        setMessage(`We couldn't fetch the shows :( ${response.statusText} ${response.status}`);
      }
    } catch (error) {
      console.error("Error fetching shows:", error);
    }
  };

  useEffect(() => {
    fetchShows(); // Fetch shows when the component mounts
  }, []);

  return (
    <div>
      <h1>Shows</h1>
      <div>{message}</div>
      <ul>
        {shows.length > 0 ? (
          shows.map((show, index) => (
            <li key={index}>
              <strong>{show.title}</strong>
              <p>{show.description}</p>
              <p><strong>Price:</strong> ${show.price}</p>
              <p><strong>Venue:</strong> {show.venue?.name}</p>
              <ul>
                {show.theatreShowDates?.$values.map((showDate: any, idx: number) => (
                  <li key={idx}>
                    <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                  </li>
                ))}
              </ul>
            </li>
          ))
        ) : (
          <li>Loading shows...</li>
        )}
      </ul>
    </div>
  );
};

export default Shows;

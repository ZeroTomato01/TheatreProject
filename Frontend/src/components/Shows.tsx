import React, { useState, useEffect } from 'react';

const Shows: React.FC = () => {
  const [shows, setShows] = useState<string[]>([]); // Store a list of shows

  const fetchShows = async () => {
    try {
      const response = await fetch("/TheatreShow", {
        method: "GET",
      });

      if (response.ok) {
        const data = await response.json();
        setShows(data); // Assume `data` is an array of show names or objects
      } else {
        console.error("Failed to fetch shows");
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
      <ul>
        {shows.length > 0 ? (
          shows.map((show, index) => (
            <li key={index}>{show}</li> // Display each show
          ))
        ) : (
          <li>Loading shows...</li>
        )}
      </ul>
    </div>
  );
};

export default Shows;
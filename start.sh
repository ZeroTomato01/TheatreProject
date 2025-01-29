#!/bin/bash

# Navigate to the backend directory and start the backend server
echo "Starting backend server..."
dotnet watch run &
BACKEND_PID=$!

# Navigate to the frontend directory and start the frontend server
echo "Starting frontend server..."
cd Frontend
npm run serve &
FRONTEND_PID=$!

# Wait for both processes to finish
wait $BACKEND_PID
wait $FRONTEND_PID
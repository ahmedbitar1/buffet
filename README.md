# Buffet System for Meeting Halls

## Project Overview

The **Buffet System for Meeting Halls** is a web-based application designed to allow users to order drinks and refreshments for the meeting halls. The system uses a QR code scanning mechanism for ordering, where each meeting room has a unique QR code. When a user scans the QR code, they can place an order that gets sent directly to the buffet screen associated with that room. The page is accessible only by scanning the QR code, and the session remains open for 30 minutes before it automatically closes.

This system is built using **Web Forms** for the frontend and **SQL Server** for the backend database to manage user data, orders, and meeting room information.

## Features

- **QR Code Scanning**: Users can scan the QR code available in the meeting room to place orders for drinks and refreshments.
- **Order Submission**: Once an order is placed, it is automatically sent to the buffet screen associated with the selected meeting room.
- **Timed Session**: The QR code session remains open for 30 minutes before the page automatically closes.
- **Admin Interface**: Admins can view and manage all orders placed through the system and update the buffet options.

## Technologies Used

- **Frontend**: Web Forms (ASP.NET)
- **Backend**: SQL Server Database
- **Programming Languages**: C#, HTML, CSS, JavaScript
- **Libraries & Frameworks**: ASP.NET Web Forms, ADO.NET
- **Database**: SQL Server

## Installation

### Set Up the Database:

1. Open **SQL Server Management Studio (SSMS)**.
2. Create a new database and import the provided `.sql` scripts to set up the necessary tables.
3. Ensure that the **connection string** in the `web.config` file matches your local database settings.

### Open the Project:

1. Open the project in **Visual Studio**.
2. Build the project to restore any NuGet packages or dependencies.

### Run the Application:

1. Press **F5** to run the application in Debug mode.
2. Open a browser and navigate to the provided local server URL to start using the system.

## Usage

### 1. **Scan the QR Code**
   - Users need to scan the QR code provided in the meeting room.
   - The QR code links to a web page where the user can place their order.
   - The QR code session is valid for 30 minutes, after which the page will automatically close.

### 2. **Place an Order**
   - After scanning the QR code, users will be presented with a list of available drinks or refreshments.
   - Users can select their order and submit it.
   - The order will be sent directly to the buffet screen in the meeting room with the room name and chair number.

### 3. **Admin Features**
   - Admin users have the ability to:
     - View and manage all orders.
     - Edit or update the list of available drinks and refreshments.
     - Monitor the status of orders in real-time.

## Database Schema

The database consists of the following main tables:

- **Users**: Stores user credentials and roles.
- **MeetingRooms**: Contains information about available meeting rooms (e.g., room name, location).
- **Orders**: Stores information about orders placed, including the selected drink/refreshment and the user's chair number.
- **BuffetScreen**: Contains details about the buffet screens, including room names and current orders.

## Future Enhancements

- **Real-time Order Updates**: Allow for real-time updates of orders on the buffet screens.
- **User Authentication**: Add user authentication for better security, where users must log in before placing orders.
- **Advanced Reporting**: Generate detailed reports for admins on order frequency, most popular items, etc.
- **Mobile App Support**: Create a mobile app for scanning QR codes and placing orders from mobile devices.

## Contributing

We welcome contributions to improve the system! If you'd like to contribute, follow these steps:

1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Commit your changes.
5. Push your changes to your fork.
6. Open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgements

- Thanks to the open-source community for various libraries and tools that helped in building this system.
- Special thanks to SQL Server documentation and Microsoft for ASP.NET Web Forms framework.
---

## 📬 **Contact**
- **Project Owner:** Ahmed Essam
- **Email:** [ahmedesamo778@gmail.com](mailto:ahmedesamo778@gmail.com)

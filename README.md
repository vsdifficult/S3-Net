# S3 Storage Manager API

This is a simple Web API built with ASP.NET Core that interacts with Amazon S3 to perform operations like uploading, downloading, deleting, and listing files in an S3 bucket.

## Features

- **Upload a file** to an S3 bucket
- **Download a file** from an S3 bucket
- **Delete a file** from an S3 bucket
- **List all files** in an S3 bucket

## Technologies Used

- **ASP.NET Core** for building the Web API
- **AWS SDK for .NET** (`AWSSDK.S3`) to interact with Amazon S3
- **Swagger** for API documentation
- **Microsoft.Extensions.Configuration** to load settings from `appsettings.json`

## Prerequisites

To run this project, you will need:

- .NET 6 or higher
- An AWS account with access to S3
- A valid **Access Key**, **Secret Key**, **Region**, and **Bucket Name** to connect to your AWS S3 bucket

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/vsdifficut/S3-Net.git
    cd S3-Net/S3StorageManager
    ```

2. Restore the dependencies:

    ```bash
    dotnet restore
    ```

3. Install the necessary NuGet packages:

    ```bash
    dotnet add package AWSSDK.S3
    dotnet add package Microsoft.Extensions.Configuration
    dotnet add package Microsoft.Extensions.Configuration.Json
    dotnet add package Swashbuckle.AspNetCore
    ```

4. Set up your AWS credentials in the `appsettings.json` file:

    ```json
    {
      "AWS": {
        "AccessKey": "YOUR_ACCESS_KEY",
        "SecretKey": "YOUR_SECRET_KEY",
        "Region": "us-east-1",
        "BucketName": "your-bucket-name"
      }
    }
    ```

5. Build and run the project:

    ```bash
    dotnet run
    ```

6. The API will be accessible at `http://localhost:5119`

7. Open the Swagger documentation by navigating to:

    ```
    http://localhost:5119/swagger
    ```

## API Endpoints

### 1. **Upload a File**
- **Method**: `POST`
- **URL**: `/api/s3/upload?keyName={keyName}`
- **Body**: Form-data (file)
- **Description**: Uploads a file to the S3 bucket.

### 2. **Download a File**
- **Method**: `GET`
- **URL**: `/api/s3/download?keyName={keyName}`
- **Description**: Downloads a file from the S3 bucket.

### 3. **Delete a File**
- **Method**: `DELETE`
- **URL**: `/api/s3/delete?keyName={keyName}`
- **Description**: Deletes a file from the S3 bucket.

### 4. **List All Files**
- **Method**: `GET`
- **URL**: `/api/s3/list`
- **Description**: Lists all files in the S3 bucket.



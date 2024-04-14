﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Room
{
    public string RoomId { get; set; }
    public string RoomName { get; set; }
    public int Capacity { get; set; }
}

public class Reservation
{
    public DateTime Time { get; set; }
    public DateTime Date { get; set; }
    public string ReserverName { get; set; }
    public Room Room { get; set; }
}

public class ReservationHandler
{
    private List<Reservation> reservations = new List<Reservation>();

    public void AddReservation(Reservation reservation)
    {
        if (reservations.Any(r => r.Room.RoomId == reservation.Room.RoomId && r.Date == reservation.Date && r.Time == reservation.Time))
        {
            Console.WriteLine("Reservation conflict, There's already a reservation for this room at the same time !!!");
        }
        else
        {
            reservations.Add(reservation);
            Console.WriteLine("Reservation added successfully.");
        }
    }

    public void DeleteReservation(Reservation reservation)
    {
        var toRemove = reservations.FirstOrDefault(r => r.Room.RoomId == reservation.Room.RoomId && r.Date == reservation.Date && r.Time == reservation.Time);
        if (toRemove != null)
        {
            reservations.Remove(toRemove);
            Console.WriteLine("Reservation deleted successfully.");
        }
        else
        {
            Console.WriteLine("Reservation not found.");
        }
    }

    public void DisplayWeeklySchedule()
    {
        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations.");
        }
        else
        {
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Room ID: {reservation.Room.RoomId}, Name: {reservation.Room.RoomName}, Reserved by: {reservation.ReserverName}, Date: {reservation.Date}, Time: {reservation.Time}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var handler = new ReservationHandler();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nReservation System");
            Console.WriteLine("1. Add Reservation");
            Console.WriteLine("2. Delete Reservation");
            Console.WriteLine("3. Display Weekly Schedule");
            Console.WriteLine("4. Exit");

            Console.Write("\nSelect an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddReservation(handler);
                    break;
                case "2":
                    DeleteReservation(handler);
                    break;
                case "3":
                    handler.DisplayWeeklySchedule();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    static void AddReservation(ReservationHandler handler)
    {
        var reservation = new Reservation();
        reservation.Room = new Room();

        Console.Write("Enter Room ID: ");
        reservation.Room.RoomId = Console.ReadLine();

        Console.Write("Enter Room Name: ");
        reservation.Room.RoomName = Console.ReadLine();

        Console.Write("Enter Reserver Name: ");
        reservation.ReserverName = Console.ReadLine();

        Console.Write("Enter Date (yyyy-mm-dd): ");
        reservation.Date = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter Time (hh:mm): ");
        reservation.Time = DateTime.Parse(Console.ReadLine());

        handler.AddReservation(reservation);
    }

    static void DeleteReservation(ReservationHandler handler)
    {
        var reservation = new Reservation();
        reservation.Room = new Room();

        Console.Write("Enter Room ID for reservation to delete: ");
        reservation.Room.RoomId = Console.ReadLine();

        Console.Write("Enter Date for reservation to delete (yyyy-mm-dd): ");
        reservation.Date = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter Time for reservation to delete (HH:mm): ");
        reservation.Time = DateTime.Parse(Console.ReadLine());

        handler.DeleteReservation(reservation);
    }
}

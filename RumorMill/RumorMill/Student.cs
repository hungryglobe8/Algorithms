using System;
using System.Collections.Generic;

namespace RumorMill
{
    public class Student
    {
        public SortedList<string, Student> Friends { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public Student(string name)
        {
            Friends = new SortedList<string, Student>();
            Name = name;
        }

        /// <summary>
        /// Add a friend connection between two students.
        /// </summary>
        /// <param name="friend"></param>
        public void MakeConnection(Student friend)
        {
            this.AddFriend(friend);
            friend.AddFriend(this);
        }

        /// <summary>
        /// Makes a single friend connection one-way.
        /// </summary>
        /// <param name="friend"></param>
        private void AddFriend(Student friend)
        {
            Friends.Add(friend.Name, friend);
        }
    }
}
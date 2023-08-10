using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Note_Taking_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
    }

        private void txt_message_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                string connectingString = "Data Source=DESKTOP-8OSKBF8\\SQLEXPRESS;Initial Catalog=\"Note taking db\";Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectingString))
                {
                    conn.Open();
                    string title = txt_title.Text;
                    string message = txt_message.Text;
                    string insertQuery = "INSERT INTO NoteTable (NoteTitle, NoteMessage) VALUES (@NoteTitle, @NoteMessage)";

                    using (SqlCommand command = new SqlCommand(insertQuery, conn))
                    {
                        command.Parameters.AddWithValue("@NoteTitle", title);
                        command.Parameters.AddWithValue("@NoteMessage", message);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Added successfully");
                        listBox1.Items.Add(title);
                        txt_message.Clear();
                        txt_title.Clear(); 

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            }

        private void btn_read_Click(object sender, EventArgs e)
        {
            try
            {
                string connectingString = "Data Source=DESKTOP-8OSKBF8\\SQLEXPRESS;Initial Catalog=\"Note taking db\";Integrated Security=True";
                using (SqlConnection conn1 = new SqlConnection(connectingString))
                {
                    conn1.Open();
                    string title = listBox1.SelectedItem.ToString();
                    string SelectQuery = "SELECT NoteTitle, NoteMessage FROM NoteTable WHERE NoteTitle = @Title";

                    using (SqlCommand command = new SqlCommand(SelectQuery, conn1))
                    {
                        command.Parameters.AddWithValue("@Title", title);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string noteTitle = reader["NoteTitle"].ToString();
                                string noteMessage = reader["NoteMessage"].ToString();

                                txt_title.Text = noteTitle;
                                txt_message.Text = noteMessage;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            txt_title.Clear();
            txt_message.Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string connectingString = "Data Source=DESKTOP-8OSKBF8\\SQLEXPRESS;Initial Catalog=\"Note taking db\";Integrated Security=True";
                using (SqlConnection conn2 = new SqlConnection(connectingString))
                {
                    conn2.Open();
                    string title = listBox1.SelectedItem.ToString();

                    string DeleteQuery = "DELETE NoteTable WHERE NoteTitle = @Title";

                    using (SqlCommand command = new SqlCommand(DeleteQuery, conn2))
                    {
                        command.Parameters.AddWithValue("@Title", title);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No record found to delete.");
                        }
                        listBox1.Items.Remove(title);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                string connectingString = "Data Source=DESKTOP-8OSKBF8\\SQLEXPRESS;Initial Catalog=\"Note taking db\";Integrated Security=True";
                using (SqlConnection conn3 = new SqlConnection(connectingString))
                {
                    conn3.Open();
                    string oldTitle = listBox1.SelectedItem.ToString();
                    string newTitle = txt_title.Text;
                    string newMessage = txt_message.Text;

                    string UpdateQuery = "UPDATE NoteTable SET NoteTitle = @NewTitle, NoteMessage = @NewMessage WHERE NoteTitle = @OldTitle";

                    using (SqlCommand command = new SqlCommand(UpdateQuery, conn3))
                    {
                        command.Parameters.AddWithValue("@OldTitle", oldTitle);
                        command.Parameters.AddWithValue("@NewTitle", newTitle);
                        command.Parameters.AddWithValue("@NewMessage", newMessage);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
                            listBox1.Items.Remove(oldTitle);
                            listBox1.Items.Add(newTitle);

                        }
                        else
                        {
                            MessageBox.Show("No record found to update.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
    }
    

﻿using LiveCharts;
using LiveCharts.Wpf;
using Noto.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Noto.Views.Pages
{
    public partial class ChartPage : Page
    {
        OracleConnection con = new OracleConnection();
         
        public SeriesCollection SeriesCollection { get; set; }
        public Func<int, string> YFormatter { get; set; }
        List<string> labels = new List<string>();
        List<int> taskCount = new List<int>();

        public ChartPage()
        {
            con.ConnectionString = DataWorker.ConnectionToOracle.connectionString;
            InitializeComponent();
        }
        int GetCountTasks(string _date)
        {
            try
            {
                con.Open();

                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "DBNoto.count_task_by_date";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_team_id", OracleDbType.Int32, 10).Value = DataWorker.CurrentTeam.teamId;
                cmd.Parameters.Add("p_deadlinedate", OracleDbType.Varchar2, 30).Value = _date;

                cmd.Parameters.Add("o_count_tasks", OracleDbType.Int32, 10);
                cmd.Parameters["o_count_tasks"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                con.Close();

                return Convert.ToInt32((decimal)(OracleDecimal)cmd.Parameters["o_count_tasks"].Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                return 0;
            }

        }
        
        private void SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDate = calendar.SelectedDates;
            
            if (SeriesCollection != null) MessageBox.Show(SeriesCollection[SeriesCollection.Count()-1].ToString());
            labels.Clear();
            foreach (var date in selectedDate)
            {
                labels.Add(date.ToString("dd.MM.yyyy"));
                taskCount.Add(GetCountTasks(date.ToString("dd.MM.yyyy")));
            }
            lablesChart.Labels = labels;

            ChartValues<int> d = new ChartValues<int>();

            d.Clear();
            foreach (int x in taskCount)
            {
                d.Add(x);
            }
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = d
                }
            };

            YFormatter = value => value.ToString("C");

            chartDiagram.Series = SeriesCollection;
            calendar.SelectedDates.Clear();
        }
    }
    
}


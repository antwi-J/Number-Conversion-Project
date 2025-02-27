using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleNumberConverter
{
	public class MainForm : Form
	{
		// declare all the controls we will use on the form
		private TextBox inputBox, binaryBox1, binaryBox2; // textBoxes for user input
		private Label inputLabel, resultLabel, nameLabel, binaryBox1Label, binaryBox2Label; // labels for text descriptions
		private ComboBox comboBox1; // dropdown to select conversion type
		private Button convertButton, addBinaryButton, resetButton; // Buttons for actions

		public MainForm()
		{
			// set up the form's title and size
			this.Text = "Number Converter Mini Project";
			this.WindowState = FormWindowState.Maximized; // Start the form maximized
			this.BackColor = Color.FloralWhite; // Set the background color

			// label to show my name
			nameLabel = new Label
			{
				Text = "Created by Joshua Antwi",
				Font = new Font("Arial", 12, FontStyle.Bold),
				Top = 20, // position from the top of the form
				Left = 20, // position from the left of the form
				AutoSize = true // automatically adjust the width of the label
			};

			// label to ask the user to enter a value
			inputLabel = new Label
			{
				Text = "Enter a value to convert:",
				Font = new Font("Arial", 12),
				Top = 70,
				Left = 20,
				AutoSize = true
			};

			// textBox for the user to type the input value
			inputBox = new TextBox
			{
				Font = new Font("Arial", 12),
				Top = 100,
				Left = 20,
				Width = 300,
				Height = 30
			};
			inputBox.KeyPress += InputBox_KeyPress; // attach event handler to restrict input based on conversion type

			// dropdown (ComboBox) to select the type of conversion
			comboBox1 = new ComboBox
			{
				Font = new Font("Arial", 12),
				Top = 150,
				Left = 20,
				Width = 300,
				Height = 30,
				DropDownStyle = ComboBoxStyle.DropDownList // user can only select from the list
			};
			// add conversion options to the dropdown
			comboBox1.Items.AddRange(new string[]
			{
				"Binary to Decimal",
				"Decimal to Binary",
				"Binary to Hex",
				"Hex to Binary",
				"Decimal to Hex",
				"Hex to Decimal"
			});
			comboBox1.SelectedIndex = 0; // set the first option as default
			comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged; // attach event handler for dropdown change

			// button to perform the conversion
			convertButton = new Button
			{
				Text = "Convert",
				Font = new Font("Arial", 12),
				Top = 200,
				Left = 20,
				Width = 150,
				Height = 40,
				BackColor = Color.LimeGreen, // set the button color to green
				ForeColor = Color.White // set the text color to white
			};
			convertButton.Click += ConvertHandler; // attach the event handler for the button click

			// label for the first binary input
			binaryBox1Label = new Label
			{
				Text = "First binary number (max 8 bits):",
				Font = new Font("Arial", 12),
				Top = 270,
				Left = 20,
				AutoSize = true
			};

			// textBox for the first binary input
			binaryBox1 = new TextBox
			{
				Font = new Font("Arial", 12),
				Top = 300,
				Left = 20,
				Width = 300,
				Height = 30
			};
			binaryBox1.KeyPress += BinaryInputHandler; // only allow 1s and 0s for input
			binaryBox1.MaxLength = 8; // limit input to 8 bits

			// label for the second binary input
			binaryBox2Label = new Label
			{
				Text = "Second binary number (max 8 bits):",
				Font = new Font("Arial", 12),
				Top = 350,
				Left = 20,
				AutoSize = true
			};

			// textBox for the second binary input
			binaryBox2 = new TextBox
			{
				Font = new Font("Arial", 12),
				Top = 380,
				Left = 20,
				Width = 300,
				Height = 30
			};
			binaryBox2.KeyPress += BinaryInputHandler; // attach event handler to restrict input to 0s and 1s
			binaryBox2.MaxLength = 8; // limit input to 8 bits

			// button to add two binary numbers
			addBinaryButton = new Button
			{
				Text = "Add Binaries",
				Font = new Font("Arial", 12),
				Top = 430,
				Left = 20,
				Width = 150,
				Height = 40,
				BackColor = Color.LimeGreen, // set the button color to green
				ForeColor = Color.White // set the text color to white
			};
			addBinaryButton.Click += AddBinaryHandler; // attach event handler for the button click

			// button to reset all inputs and results
			resetButton = new Button
			{
				Text = "Reset",
				Font = new Font("Arial", 12),
				Top = 490,
				Left = 20,
				Width = 150,
				Height = 40,
				BackColor = Color.Red, // set the button color to red
				ForeColor = Color.White // set the text color to white
			};
			resetButton.Click += ResetHandler; // attach event handler for the button click

			// label to display the result of conversions or binary addition
			resultLabel = new Label
			{
				Text = "Result will appear here.",
				Font = new Font("Arial", 12),
				Top = 550,
				Left = 20,
				Width = 600,
				Height = 40,
				BorderStyle = BorderStyle.FixedSingle, // add a border around the label
				BackColor = Color.White // set the background color to white
			};

			// add all the controls to the form
			this.Controls.Add(nameLabel);
			this.Controls.Add(inputLabel);
			this.Controls.Add(inputBox);
			this.Controls.Add(comboBox1);
			this.Controls.Add(convertButton);
			this.Controls.Add(binaryBox1Label);
			this.Controls.Add(binaryBox1);
			this.Controls.Add(binaryBox2Label);
			this.Controls.Add(binaryBox2);
			this.Controls.Add(addBinaryButton);
			this.Controls.Add(resetButton);
			this.Controls.Add(resultLabel);
		}

		// event handler for the Convert button
		private void ConvertHandler(object sender, EventArgs e)
		{
			string input = inputBox.Text.Trim(); // get the user's input
			string selectedOption = comboBox1.SelectedItem.ToString(); //get the selected conversion type
			string result = ""; // variable to store the result

			//
			//check if the input is empty
			if (string.IsNullOrEmpty(input))
			{
				MessageBox.Show("Please enter a value!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				//oerform the selected conversion
				switch (selectedOption)
				{
					case "Binary to Decimal":
						if (!IsBinary(input))
							throw new Exception("Invalid binary input");
						result = BinaryToDecimal(input).ToString(); // convert binary to decimal
						break;
					case "Decimal to Binary":
						int decimalValue = int.Parse(input); // convert input to integer
						result = DecimalToBinary(decimalValue); // convert decimal to binary
						break;
					case "Binary to Hex":
						if (!IsBinary(input))
							throw new Exception("Invalid binary input");
						result = DecimalToHex(BinaryToDecimal(input)); //convert binary to hex
						break;
					case "Hex to Binary":
						result = DecimalToBinary(HexToDecimal(input)); //convert hex to binary
						break;
					case "Decimal to Hex":
						decimalValue = int.Parse(input); //convert input to integer
						result = DecimalToHex(decimalValue); // Convert decimal to hex
						break;
					case "Hex to Decimal":
						result = HexToDecimal(input).ToString(); //convert hex to decimal
						break;
				}

				//display the result
				resultLabel.Text = "Result: " + result;
			}
			catch (Exception ex)
			{
				//show an error message if something goes wrong
				MessageBox.Show("Invalid input. Please check your value! " + ex.Message, "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// event handler for the Add Binary button
		private void AddBinaryHandler(object sender, EventArgs e)
		{
			string binary1 = binaryBox1.Text.Trim(); //get the first binary input
			string binary2 = binaryBox2.Text.Trim(); //get the second binary input

			//check if both binary inputs are provided
			if (string.IsNullOrEmpty(binary1) || string.IsNullOrEmpty(binary2))
			{
				MessageBox.Show("Both binary inputs are required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//check if binary inputs are within the 8-bit limit
			if (binary1.Length > 8 || binary2.Length > 8)
			{
				MessageBox.Show("Binary inputs must be 8 bits or less!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//add the two binary numbers and display the result
			string result = AddBinary(binary1, binary2);
			resultLabel.Text = "Result: " + result;
		}

		//event handler for the Reset button
		private void ResetHandler(object sender, EventArgs e)
		{
			// Clear all inputs and the result label
			inputBox.Text = "";
			binaryBox1.Text = "";
			binaryBox2.Text = "";
			resultLabel.Text = "Result will appear here.";
		}

		//event handler to restrict binary input to 0s and 1s
		private void BinaryInputHandler(object sender, KeyPressEventArgs e)
		{
			// Only allow 0, 1, or backspace to be entered
			if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != (char)Keys.Back)
			{
				e.Handled = true; // Ignore the input
			}
		}

		//event handler to restrict input based on the selected conversion type
		private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			string selectedOption = comboBox1.SelectedItem.ToString();

			//if the conversion type is binary-to-something, restrict input to 0 and 1
			if (selectedOption.StartsWith("Binary"))
			{
				if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != (char)Keys.Back)
				{
					e.Handled = true; // Ignore the input
				}
			}
		}

		//event handler for when the ComboBox selection changes
		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//clear the input box when the conversion type changes
			inputBox.Text = "";
		}

		//function  to check if a string is a valid binary number
		private bool IsBinary(string binary)
		{
			foreach (char c in binary)
			{
				if (c != '0' && c != '1')
					return false; //return false if any character is not 0 or 1
			}
			return true; //return true if all characters are 0 or 1
		}

		//function to convert binary to decimal
		private int BinaryToDecimal(string binary)
		{
			int decimalValue = 0;
			int multiplier = 1;

			//loop through each character in the binary string
			for (int i = binary.Length - 1; i >= 0; i--)
			{
				if (binary[i] == '1')
					decimalValue += multiplier; //add the multiplier if the bit is 1
				multiplier *= 2; //double the multiplier for the next bit
			}

			return decimalValue; //return the decimal value
		}

		//function to convert decimal to binary
		private string DecimalToBinary(int decimalValue)
		{
			if (decimalValue == 0) return "0"; //return "0" if the input is 0

			string binary = "";

			// Convert the decimal number to binary
			while (decimalValue > 0)
			{
				binary = (decimalValue % 2) + binary; // Add the remainder to the binary string
				decimalValue /= 2; // Divide the decimal value by 2
			}

			return binary; // Return the binary string
		}

		//function to convert decimal to hexadecimal
		private string DecimalToHex(int decimalValue)
		{
			if (decimalValue == 0) return "0"; //return "0" if the input is 0

			string hex = "";
			string hexDigits = "0123456789ABCDEF"; //all possible hex digits

			//convert the decimal number to hexadecimal
			while (decimalValue > 0)
			{
				hex = hexDigits[decimalValue % 16] + hex; //addthe corresponding hex digit
				decimalValue /= 16; //divide the decimal value by 16
			}

			return hex; //return the hexadecimal string
		}

		//function to convert hexadecimal to decimal
		private int HexToDecimal(string hex)
		{
			int decimalValue = 0;
			int multiplier = 1;

			//loop through each character in the hex string
			for (int i = hex.Length - 1; i >= 0; i--)
			{
				char c = char.ToUpper(hex[i]); //convert the character to uppercase
				int value = (c >= '0' && c <= '9') ? c - '0' : c - 'A' + 10; //get the numeric value of the hex digit
				decimalValue += value * multiplier; //add the value to the decimal result
				multiplier *= 16; //multiply the multiplier by 16 for the next digit
			}

			return decimalValue; //return the decimal value
		}

		//function to add two binary numbers
		private string AddBinary(string binary1, string binary2)
		{
			int carry = 0;
			string result = "";

			//make sure both binary numbers are the same length by adding leading zeros
			while (binary1.Length < binary2.Length) binary1 = "0" + binary1;
			while (binary2.Length < binary1.Length) binary2 = "0" + binary2;

			//loop through each bit of the binary numbers
			for (int i = binary1.Length - 1; i >= 0; i--)
			{
				int bit1 = binary1[i] - '0'; // get the first bit
				int bit2 = binary2[i] - '0'; // get the second bit

				int sum = bit1 + bit2 + carry; // add the bits and the carry
				result = (sum % 2) + result; // add the result bit to the final result
				carry = sum / 2; // calculate the new carry
			}

			if (carry > 0)
				result = "1" + result; // add the final carry if it exists

			return result; //return the result of the binary addition
		}

		//main method to start the application
		public static void Main()
		{
			Application.EnableVisualStyles(); //enable visual styles for the application
			Application.Run(new MainForm()); //run the form
		}
	}
}
using System;
using System.Windows.Forms;

namespace Задание_1
{
    // Класс формы Form1 должен быть первым
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Прячем label5 и label6 при загрузке формы
            label5.Visible = false;
            label6.Visible = false;
            // Обработчики событий для кнопок
            button1.Click += new EventHandler(this.button1_Click);
            button2.Click += new EventHandler(this.button2_Click);
        }
        // Метод для обработки нажатия кнопки "Вычислить площадь"
        private void button1_Click(object sender, EventArgs e)
        {
            IShape shape = CreateShapeFromInput();
            if (shape != null)
            {
                double area = shape.CalculateArea();
                label5.Text = $"Площадь: {area:F2}";
                label5.Visible = true;
            }
        }
        // Метод для обработки нажатия кнопки "Вычислить периметр"
        private void button2_Click(object sender, EventArgs e)
        {
            IShape shape = CreateShapeFromInput();
            if (shape != null)
            {
                double perimeter = shape.CalculatePerimeter();
                label6.Text = $"Периметр: {perimeter:F2}";
                label6.Visible = true;
            }
        }
        // Метод для создания объекта фигуры на основе выбора пользователя
        private IShape CreateShapeFromInput()
        {
            double param1, param2, param3;
            // Проверяем корректность ввода всех трех параметров
            if (!double.TryParse(textBox1.Text, out param1) ||
                !double.TryParse(textBox2.Text, out param2) ||
                !double.TryParse(textBox3.Text, out param3))
            {
                MessageBox.Show("Введите корректные числовые значения для параметров!");
                return null;
            }
            string selectedShape = comboBox1.SelectedItem?.ToString();
            switch (selectedShape)
            {
                case "Круг":
                    return new Circle(param1); // param1 - радиус
                case "Прямоугольник":
                    return new Rectangle(param1, param2); // param1 - ширина, param2 - высота
                case "Треугольник":
                    return new Triangle(param1, param2, param3); // param1, param2 и param3 - три стороны
                default:
                    MessageBox.Show("Выберите фигуру из списка!");
                    return null;
            }
        }
    }
    // Интерфейс для вычисления площади и периметра
    public interface IShape
    {
        double CalculateArea();
        double CalculatePerimeter();
    }
    // Класс для круга
    public class Circle : IShape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            Radius = radius;
        }
        public double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
        public double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }
    }
    // Класс для прямоугольника
    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }
        public double CalculateArea()
        {
            return Width * Height;
        }
        public double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }
    }
    // Класс для треугольника
    public class Triangle : IShape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }
        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }
        public double CalculateArea()
        {
            double semiPerimeter = CalculatePerimeter() / 2;
            return Math.Sqrt(semiPerimeter * (semiPerimeter - SideA) * (semiPerimeter - SideB) * (semiPerimeter - SideC));
        }
        public double CalculatePerimeter()
        {
            return SideA + SideB + SideC;
        }
    }
}
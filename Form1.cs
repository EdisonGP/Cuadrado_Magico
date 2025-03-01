using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POO_19B_CuadradoMagico
{
    public partial class Form1 : Form
    {
        // Variables para almacenar el tamaño del cuadrado mágico
        private int num;
        // Matriz para almacenar los valores del cuadrado mágico
        private int[,] cuadro;
        // Matriz para almacenar las cajas de texto del cuadrado mágico
        private TextBox[][] textBox;
        // Variable para controlar la velocidad de generación del cuadrado mágico
        int valor;
        // Arreglos para almacenar las sumas de filas y columnas
        int[] arreglo1;
        int[] arreglo2;
        // Arreglos para almacenar los labels de sumas de filas y columnas
        private Label[] filasLabels;
        private Label[] columnasLabels;
        // Constantes para dimensiones de UI
        private int MARGEN_X = 50;
        private int MARGEN_Y = 150;
        private const int TAMANO_CELDA = 45;
        private const int MARGEN_ETIQUETAS = 15;

        // Constructor de la clase Form1
        public Form1()
        {
            // Inicializa los componentes del formulario
            InitializeComponent();
            // Deshabilita el botón de comprobar al inicio
            btnComprobar.Enabled = false;
            // Configura el formulario con un diseño más profesional
            ConfigurarInterfaz();
        }

        // Método para configurar la interfaz con un diseño más elegante y profesional
        private void ConfigurarInterfaz()
        {
            // Establece el título del formulario
            this.Text = "Cuadrado Mágico";
            // Establece el color de fondo del formulario
            this.BackColor = Color.FromArgb(248, 249, 250);
            // Deshabilita la opción de redimensionar el formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // Establece el icono del formulario como el predeterminado
            this.Icon = SystemIcons.Application;
            // Tamaño inicial del formulario
            this.ClientSize = new Size(700, 600);
            // Centra el formulario en la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel superior con degradado
            Panel panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(63, 81, 181)
            };
            this.Controls.Add(panelSuperior);

            // Título principal en el panel superior
            Label lblTituloPrincipal = new Label
            {
                Text = "GENERADOR DE CUADRADOS MÁGICOS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(90, 20),
                Parent = panelSuperior
            };

            // Panel de control a la izquierda
            Panel panelControl = new Panel
            {
                Width = 240,
                Height = 600,
                Location = new Point(0, 80),
                BackColor = Color.FromArgb(238, 238, 238),
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(panelControl);

            // Aplicar sombra al panel
            panelControl.Paint += (sender, e) => {
                ControlPaint.DrawBorder(e.Graphics, panelControl.ClientRectangle,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.Gray, 1, ButtonBorderStyle.Solid,
                    Color.Gray, 1, ButtonBorderStyle.Solid);
            };

            // Título del panel de control
            Label lblControlPanel = new Label
            {
                Text = "CONFIGURACIÓN",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(63, 81, 181),
                AutoSize = true,
                Location = new Point(30, 15),
                Parent = panelControl
            };

            // Descripción del cuadrado mágico
            Label lblDescripcion = new Label
            {
                Text = "Un cuadrado donde todas las filas, columnas y diagonales suman el mismo valor.",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(70, 70, 70),
                Width = 200,
                Height = 50,
                Location = new Point(15, 45),
                Parent = panelControl
            };

            // Etiqueta para el tamaño
            Label lblTamano = new Label
            {
                Text = "Tamaño:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                Location = new Point(15, 105),
                Parent = panelControl
            };

            // Reposicionar y estilizar el txtNumero
            txtNumero.Location = new Point(85, 102);
            txtNumero.Size = new Size(60, 30);
            txtNumero.Font = new Font("Segoe UI", 11);
            txtNumero.BorderStyle = BorderStyle.FixedSingle;
            txtNumero.TextAlign = HorizontalAlignment.Center;
            txtNumero.Parent = panelControl;

            // Nota sobre números impares
            Label lblNota = new Label
            {
                Text = "(Ingrese un número impar)",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Location = new Point(15, 130),
                Parent = panelControl
            };

            // Etiqueta para la velocidad
            Label lblVelocidad = new Label
            {
                Text = "Velocidad:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                Location = new Point(15, 160),
                Parent = panelControl
            };

            // Reposicionar y estilizar el trackBar
            trackBar1.Location = new Point(15, 185);
            trackBar1.Size = new Size(190, 45);
            trackBar1.BackColor = Color.FromArgb(238, 238, 238);
            trackBar1.Parent = panelControl;

            // Reposicionar y estilizar el checkbox
            ckbJugar.Location = new Point(15, 240);
            ckbJugar.Size = new Size(190, 30);
            ckbJugar.Font = new Font("Segoe UI", 10);
            ckbJugar.Text = "Modo Manual";
            ckbJugar.ForeColor = Color.FromArgb(50, 50, 50);
            ckbJugar.Parent = panelControl;

            // Línea divisoria
            Panel divider = new Panel
            {
                Height = 1,
                Width = 190,
                BackColor = Color.FromArgb(200, 200, 200),
                Location = new Point(15, 280),
                Parent = panelControl
            };

            // Reposicionar y estilizar los botones
            button1.Text = "GENERAR";
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.FromArgb(63, 81, 181);
            button1.ForeColor = Color.White;
            button1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button1.Size = new Size(90, 35);
            button1.Location = new Point(15, 300);
            button1.FlatAppearance.BorderSize = 0;
            button1.Cursor = Cursors.Hand;
            button1.Parent = panelControl;

            button2.Text = "LIMPIAR";
            button2.FlatStyle = FlatStyle.Flat;
            button2.BackColor = Color.FromArgb(211, 47, 47);
            button2.ForeColor = Color.White;
            button2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button2.Size = new Size(90, 35);
            button2.Location = new Point(115, 300);
            button2.FlatAppearance.BorderSize = 0;
            button2.Cursor = Cursors.Hand;
            button2.Parent = panelControl;

            btnComprobar.Text = "COMPROBAR";
            btnComprobar.FlatStyle = FlatStyle.Flat;
            btnComprobar.BackColor = Color.FromArgb(56, 142, 60);
            btnComprobar.ForeColor = Color.White;
            btnComprobar.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnComprobar.Size = new Size(190, 35);
            btnComprobar.Location = new Point(15, 345);
            btnComprobar.FlatAppearance.BorderSize = 0;
            btnComprobar.Cursor = Cursors.Hand;
            btnComprobar.Parent = panelControl;

            // Panel para el cuadrado mágico
            Panel panelCuadrado = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Location = new Point(260, 100),
                Size = new Size(500, 600),
                AutoScroll = true
            };
            this.Controls.Add(panelCuadrado);

            // Aplicar sombra al panel del cuadrado
            panelCuadrado.Paint += (sender, e) => {
                ControlPaint.DrawBorder(e.Graphics, panelCuadrado.ClientRectangle,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.Gray, 1, ButtonBorderStyle.Solid,
                    Color.Gray, 1, ButtonBorderStyle.Solid);
            };

            // Ajustar el punto de origen para los TextBox
            MARGEN_X = 30;
            MARGEN_Y = 30;
        }

        // Método evento para el TextBox (usado en setMatrizTextBox)
        private void evento(object sender, EventArgs e)
        {
            // Este método se llama cada vez que cambia el texto en un TextBox
            TextBox txt = (TextBox)sender;
            int value;
            // Si el texto no es un número válido o está vacío, establece el fondo en color rosado
            if (!int.TryParse(txt.Text, out value) && !string.IsNullOrEmpty(txt.Text))
            {
                txt.BackColor = Color.FromArgb(255, 205, 210);
            }
            else
            {
                // Si el texto es un número válido, establece el fondo en color verde claro
                txt.BackColor = Color.FromArgb(200, 230, 201);
            }
        }

        // Botón generador del cuadrado mágico
        private void button1_Click(object sender, EventArgs e)
        {
            // Obtiene el valor del control deslizante para la velocidad de generación
            valor = trackBar1.Value;

            // Verifica que el cuadro de texto no esté vacío
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show("Por favor ingrese un número", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Intenta convertir el texto a un número entero
            if (!int.TryParse(txtNumero.Text, out num))
            {
                MessageBox.Show("Por favor ingrese un número válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica que el número sea impar
            if (num % 2 == 0)
            {
                MessageBox.Show("Ingresa un número impar para generar el cuadrado mágico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Limpia el formulario primero para evitar duplicados
                LimpiarFormulario();

                // Crea una matriz con las dimensiones del número de entrada
                cuadro = new int[num, num];
                // Inicializa los arreglos para las sumas
                arreglo1 = new int[num + 1];
                arreglo2 = new int[num + 1];

                // Encuentra el panel del cuadrado
                Panel panelCuadrado = null;
                foreach (Control control in Controls)
                {
                    if (control is Panel panel && panel.Location.X > 250)
                    {
                        panelCuadrado = panel;
                        break;
                    }
                }

                if (panelCuadrado != null)
                {
                    // Limpia el panel del cuadrado
                    panelCuadrado.Controls.Clear();

                    // Genera la matriz con las dimensiones establecidas
                    setMatrizTextBox(panelCuadrado);
                    // Crea las etiquetas para las sumas
                    CrearEtiquetasSumas(panelCuadrado);

                    if (ckbJugar.Checked) // Habilita para que llene manualmente los números en el TextBox
                    {
                        // Refresca la vista
                        panelCuadrado.Refresh();
                    }
                    else
                    {
                        // Refresca la vista y genera el cuadrado mágico
                        panelCuadrado.Refresh();
                        generarCuadradoMagico(num);
                        // Calcula y muestra las sumas
                        MostrarSumas();
                    }
                }
            }
        }

        // Método para crear las etiquetas de sumas para filas y columnas
        private void CrearEtiquetasSumas(Panel panel)
        {
            // Inicializa los arreglos de etiquetas
            filasLabels = new Label[num];
            columnasLabels = new Label[num];

            // Crea las etiquetas para las columnas
            for (int i = 0; i < num; i++)
            {
                filasLabels[i] = new Label();
                filasLabels[i].AutoSize = true;
                filasLabels[i].Font = new Font("Segoe UI", 10, FontStyle.Bold);
                filasLabels[i].ForeColor = Color.FromArgb(63, 81, 181);
                filasLabels[i].Location = new Point((MARGEN_X-10) + (num * TAMANO_CELDA) + MARGEN_ETIQUETAS, MARGEN_Y-10 + (i * TAMANO_CELDA) + TAMANO_CELDA / 2 - 10);
                filasLabels[i].Text = "= 0";
                panel.Controls.Add(filasLabels[i]);
            }

            // Crea las etiquetas para las filas
            for (int j = 0; j < num; j++)
            {
                columnasLabels[j] = new Label();
                columnasLabels[j].AutoSize = true;
                columnasLabels[j].Font = new Font("Segoe UI", 10, FontStyle.Bold);
                columnasLabels[j].ForeColor = Color.FromArgb(63, 81, 181);
                columnasLabels[j].Location = new Point(MARGEN_X + (j * TAMANO_CELDA) + TAMANO_CELDA / 2 - 18, (MARGEN_Y-20) + (num * TAMANO_CELDA) + MARGEN_ETIQUETAS);
                columnasLabels[j].Text = "= 0";
                panel.Controls.Add(columnasLabels[j]);
            }

            // Crea una etiqueta para la constante mágica
            Label lblConstanteMagica = new Label();
            lblConstanteMagica.AutoSize = true;
            lblConstanteMagica.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblConstanteMagica.ForeColor = Color.FromArgb(211, 47, 47);
            lblConstanteMagica.Location = new Point(MARGEN_X, MARGEN_Y + (num * TAMANO_CELDA) + MARGEN_ETIQUETAS + 30);
            int constanteMagica = num * (num * num + 1) / 2;
            lblConstanteMagica.Text = $"Constante mágica = {constanteMagica}";
            panel.Controls.Add(lblConstanteMagica);
        }

        // Método para calcular y mostrar las sumas de filas y columnas
        private void MostrarSumas()
        {
            // Matriz para almacenar los valores de los TextBox
            int[,] matriz = new int[num, num];

            // Obtiene los valores de los TextBox
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (int.TryParse(textBox[i][j].Text, out int value))
                    {
                        matriz[i, j] = value;
                    }
                }
            }

            // Inicializa los arreglos de sumas
            int[] sumasFila = new int[num];
            int[] sumasColumna = new int[num];

            // Calcula las sumas de filas y columnas
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    sumasFila[i] += matriz[i, j];
                    sumasColumna[j] += matriz[j, i];
                }
            }

            // Muestra las sumas en las etiquetas
            for (int i = 0; i < num; i++)
            {
                filasLabels[i].Text = $"= {sumasFila[i]}";
                columnasLabels[i].Text = $"= {sumasColumna[i]}";
            }
        }

        // Crear una matriz de TextBox
        public void setMatrizTextBox(Panel panel)
        {
            // Inicializa el arreglo de TextBox con el tamaño de la primera dimensión de la matriz 'cuadro'
            textBox = new TextBox[cuadro.GetLength(0)][];

            // Inicializa cada fila del arreglo de TextBox con el tamaño de la segunda dimensión de la matriz 'cuadro'
            for (int i = 0; i < cuadro.GetLength(0); i++)
            {
                textBox[i] = new TextBox[cuadro.GetLength(1)];
            }

            // Coordenadas iniciales para colocar los TextBox en el formulario
            int x = MARGEN_X;
            int y = MARGEN_Y;

            // Recorre cada fila del arreglo de TextBox
            for (int i = 0; i < textBox.Length; i++)
            {
                // Recorre cada columna del arreglo de TextBox
                for (int j = 0; j < textBox[i].Length; j++)
                {
                    // Crea un nuevo TextBox
                    textBox[i][j] = new TextBox();
                    textBox[i][j].BackColor = Color.FromArgb(200, 230, 201);
                    textBox[i][j].SetBounds(x, y, TAMANO_CELDA, TAMANO_CELDA);
                    textBox[i][j].Size = new Size(TAMANO_CELDA, TAMANO_CELDA);
                    textBox[i][j].TextAlign = HorizontalAlignment.Center;
                    textBox[i][j].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    textBox[i][j].BorderStyle = BorderStyle.FixedSingle;
                    textBox[i][j].MaxLength = 3;
                    panel.Controls.Add(textBox[i][j]);
                    textBox[i][j].TextChanged += new EventHandler(this.evento);

                    x += TAMANO_CELDA;
                }
                // Reinicia la coordenada x y aumenta la coordenada y para la siguiente fila de TextBox
                x = MARGEN_X;
                y += TAMANO_CELDA;
            }
        }

        // Método para limpiar el formulario
        private void LimpiarFormulario()
        {
            // Encuentra el panel del cuadrado
            Panel panelCuadrado = null;
            foreach (Control control in Controls)
            {
                if (control is Panel panel && panel.Location.X > 250)
                {
                    panelCuadrado = panel;
                    break;
                }
            }

            if (panelCuadrado != null)
            {
                // Limpia todos los controles del panel
                panelCuadrado.Controls.Clear();

                // Refresca la vista
                panelCuadrado.Refresh();
            }
        }

        // Botón limpiar
        private void button2_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        // Método para evaluar si el cuadrado es mágico
        public bool Evaluador(int num)
        {
            // Matriz en la cual se va a recibir los valores de los botones
            int[,] matriz = new int[num, num];
            bool resp = true; // Cambiado a true por defecto, se cambiará a false si alguna suma no coincide

            // Recolecta los valores de los TextBox
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (int.TryParse(textBox[i][j].Text, out int value))
                    {
                        matriz[i, j] = value;
                    }
                    else
                    {
                        MessageBox.Show($"El valor en la posición ({i + 1}, {j + 1}) no es un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            // Inicializa los arreglos de sumas
            arreglo1 = new int[num + 1]; // Sumas de filas + diagonal principal
            arreglo2 = new int[num + 1]; // Sumas de columnas + diagonal secundaria

            // Calcula las sumas de filas y columnas
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    arreglo1[i] += matriz[i, j]; // Suma de fila i
                    arreglo2[i] += matriz[j, i]; // Suma de columna i
                }
            }

            // Calcula la suma de la diagonal principal
            for (int i = 0; i < num; i++)
            {
                arreglo1[num] += matriz[i, i];
            }

            // Calcula la suma de la diagonal secundaria
            for (int i = 0; i < num; i++)
            {
                arreglo2[num] += matriz[i, num - 1 - i];
            }

            // Comprueba si todas las sumas son iguales
            int valorReferencia = arreglo1[0];
            for (int i = 0; i < arreglo1.Length; i++)
            {
                if (arreglo1[i] != valorReferencia || arreglo2[i] != valorReferencia)
                {
                    resp = false;
                    break;
                }
            }

            // Muestra las sumas calculadas
            MostrarSumas();

            return resp;
        }

        // Botón para comprobar el cuadrado mágico
        private void btnComprobar_Click(object sender, EventArgs e)
        {
            // Inicializa los arreglos para las sumas si no existen
            if (arreglo1 == null || arreglo2 == null)
            {
                arreglo1 = new int[num + 1];
                arreglo2 = new int[num + 1];
            }

            // Evalúa si el cuadrado es mágico
            if (Evaluador(num))
            {
                MessageBox.Show("¡Felicidades! Has completado correctamente el cuadrado mágico.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Resalta todas las celdas en verde para indicar éxito
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        textBox[i][j].BackColor = Color.FromArgb(165, 214, 167);
                    }
                }
            }
            else
            {
                MessageBox.Show("El cuadrado no es mágico. Intenta nuevamente.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Resalta todas las celdas en rojo para indicar error
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        textBox[i][j].BackColor = Color.FromArgb(255, 205, 210);
                    }
                }
            }
        }

        // Evento cuando cambia el estado del checkbox
        private void ckbJugar_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbJugar.Checked)
            {
                btnComprobar.Enabled = true;
            }
            else
            {
                btnComprobar.Enabled = false;
            }
        }

        // Evento de carga del formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Habilita el ajuste automático del tamaño del formulario
            AutoSize = false;
        }

        // Método para generar el cuadrado mágico automáticamente
        public void generarCuadradoMagico(int n)
        {
            int fila = 0;
            int columna = n / 2;
            int fil_ant = 0;
            int col_ant = n / 2;
            int cont = 1;

            // Coloca el primer número en el centro de la primera fila
            cuadro[fila, columna] = cont++;
            textBox[fila][columna].Text = cuadro[fila, columna].ToString();
            textBox[fila][columna].BackColor = Color.FromArgb(144, 202, 249);

            // Reproduce un sonido y espera un tiempo según el valor del control deslizante
            System.Media.SystemSounds.Asterisk.Play();
            Thread.Sleep(valor * 50);

            // Genera el resto del cuadrado mágico
            while (cont <= n * n)
            {
                // Mueve una posición hacia arriba y a la derecha
                fila--;
                columna++;

                // Si se sale por arriba, vuelve a la última fila
                if (fila < 0) fila = n - 1;

                // Si se sale por la derecha, vuelve a la primera columna
                if (columna > n - 1) columna = 0;

                // Si la celda ya está ocupada, se mueve a la celda de abajo de la anterior
                if (cuadro[fila, columna] != 0)
                {
                    columna = col_ant;
                    fila = fil_ant + 1;
                    // Si se sale por abajo, vuelve a la primera fila
                    if (fila == n)
                        fila = 0;
                }

                // Coloca el número en la celda
                cuadro[fila, columna] = cont;
                cont++;

                // Reproduce un sonido y actualiza la interfaz
                System.Media.SystemSounds.Asterisk.Play();
                this.Refresh();
                textBox[fila][columna].Text = cuadro[fila, columna].ToString();
                textBox[fila][columna].BackColor = Color.FromArgb(144, 202, 249);

                // Espera un tiempo según el valor del control deslizante
                Thread.Sleep(valor * 50);

                // Guarda la posición actual para la siguiente iteración
                fil_ant = fila;
                col_ant = columna;
            }
        }
    }
}
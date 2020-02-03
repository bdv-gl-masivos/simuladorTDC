using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFUI.Models;

namespace WPFUI.Views
{
    public partial class ShellView : Window
    {
        List<ConsolidadoAcrClientModel> acr = new List<ConsolidadoAcrClientModel>();
        List<ConsolidadoAcrModel> clientes = new List<ConsolidadoAcrModel>();
        List<SaldosTdcModel> saldo = new List<SaldosTdcModel>();

        public ShellView()
        {
            InitializeComponent();
            nacionalidad.Items.Add("V");
            nacionalidad.Items.Add("E");
            nacionalidad.Items.Add("P");
            nacionalidad.Text = "V";
            USER.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar_Click(sender, e);

            string cedu;
            
            cedu = "000000000" + cedulaCliente.Text;
            cedu = nacionalidad.Text + cedu.Substring(cedu.Length - 8);
            
            try
            {
                clientes = SqliteDataAccess.CargaCedula(cedu);

                if (clientes.Count > 0)
                {
                    for (int i = 0; i < clientes.Count; i++)
                    {
                        listaProducto.Items.Add(clientes[i].PRODUCTO.ToString());
                        listaNroCredito.Items.Add(clientes[i].NUMERO_CREDITO.ToString());
                        listaHistorico.Items.Add(clientes[i].HISTORICO.ToString());
                        listaMontoLiquidado.Items.Add(clientes[i].MONTO_LIQUIDADO.ToString("N2"));
                        listaFechaLiquidado.Items.Add(clientes[i].FECHA_LIQUIDACION.ToString());
                        listaFechaAumento.Items.Add(clientes[i].FECHA_ULT_AUMENTO.ToString());
                        listaMora.Items.Add(clientes[i].MORA.ToString());

                        if (clientes[i].PRODUCTO.ToString() == "TDC")
                        {
                            listaTarjetas.Items.Add(clientes[i].NUMERO_CREDITO.ToString());
                            listaLimite.Items.Add(clientes[i].MONTO_LIQUIDADO.ToString("N2"));
                        }
                    }

                    for(int i = 0; i < clientes.Count; i++) // SE AGREGA LOS PROCTOS SOCIALES AL FINAL DE LA LISTA
                    {

                        if (clientes[i].PRODUCTO.ToString() == "TDC_SOCIAL")
                        {
                            listaTarjetas.Items.Add(clientes[i].NUMERO_CREDITO.ToString());
                            listaLimite.Items.Add(clientes[i].MONTO_LIQUIDADO.ToString("N2"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("NO POSEE OPERACIONES ACTIVAS BDV");
            }

            try
            {
                acr = SqliteDataAccess.CargaCedulaFull(cedu);

                if (acr[0].CONSOLIDADO == 1)
                {
                    CALIFICA.Text = "NO CUMPLE POLITICAS";
                    CALIFICA.Background = Brushes.Red;
                }
                else
                {
                    CALIFICA.Text = "CALIFICA";
                    CALIFICA.Background = Brushes.Green;
                }

                if (acr.Count > 0)
                {
                    for (int i = 0; i < acr.Count; i++)
                    {
                        listaMes_1.Items.Add(acr[i].MES_1.ToString("N2"));
                        listaMes_2.Items.Add(acr[i].MES_2.ToString("N2"));
                        listaMes_3.Items.Add(acr[i].MES_3.ToString("N2"));
                        listaSalario.Items.Add(acr[i].SALARIO.ToString("N2"));
                        listaNombreEmpresa.Items.Add(acr[i].NOMBRE_EMPRESA.ToString());
                        listaRifEmpresa.Items.Add(acr[i].RIF_EMPRESA.ToString());
                    }

                    cargasBdv.Text = acr[0].CARGA_BDV.ToString("N2");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("NO POSEE EXPERIENCIA BDV");
            }

            try
            {
                saldo = SqliteDataAccess.SaldosTdcs(cedu);

                if (saldo.Count > 0)
                {
                    listaMes_1_saldo.Items.Add(saldo[0].MES_1.ToString("N2"));
                    listaMes_2_saldo.Items.Add(saldo[0].MES_2.ToString("N2"));
                    listaMes_3_saldo.Items.Add(saldo[0].MES_3.ToString("N2"));
                    listaSaldoPro.Items.Add(saldo[0].SALDO_PROMEDIO.ToString("N2"));
                }
            }
            catch (Exception)
            {

            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Buscar_Click(sender, e);
            }
        }

        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {
            CALIFICA.Text = "";
            CALIFICA.Background = Brushes.White;
            riesgoConsolidado.Background = Brushes.White;
            acr = new List<ConsolidadoAcrClientModel>();
            clientes = new List<ConsolidadoAcrModel>();
            saldo = new List<SaldosTdcModel>();
            listaProducto.Items.Clear();
            listaNroCredito.Items.Clear();
            listaHistorico.Items.Clear();
            listaFechaAumento.Items.Clear();
            listaFechaLiquidado.Items.Clear();
            listaMontoLiquidado.Items.Clear();
            listaFechaAumento.Items.Clear();
            listaMora.Items.Clear();

            listaMes_1.Items.Clear();
            listaMes_2.Items.Clear();
            listaMes_3.Items.Clear();
            listaSalario.Items.Clear();
            listaNombreEmpresa.Items.Clear();
            listaRifEmpresa.Items.Clear();

            listaMes_1_saldo.Items.Clear();
            listaMes_2_saldo.Items.Clear();
            listaMes_3_saldo.Items.Clear();
            listaSaldoPro.Items.Clear();
            listaLimiteOtorgado.Items.Clear();

            listaTarjetas.Items.Clear();
            listaLimite.Items.Clear();

            cargasOtras.Text = "";
            cargasBdv.Text = "";

            ingresosOtros.Text = "";
            CALIFICA.Background = Brushes.White;

            riesgoConsolidado.Text = "";

            casoEspecial.IsChecked = false;
            oficioRiesgoso.IsChecked = false;
            casoReferido.IsChecked = false;

            aprobadoreferido.Text = "";
        }

        private void Calcular_Click(object sender, RoutedEventArgs e)
        {

            riesgoConsolidado.Background = Brushes.White;

            listaLimiteOtorgado.Items.Clear(); // limpia listado de limites a otrogar para nuevo calculo

            try
            {
                int contador = 0;
                if (cargasBdv.Text == "")
                {
                    cargasBdv.Text = "0";
                }

                if (ingresosOtros.Text == "")
                {
                    ingresosOtros.Text = "0";
                }

                if (cargasOtras.Text == "")
                {
                    cargasOtras.Text = "0";
                }

                double ingresos = double.Parse(ingresosOtros.Text);
                double cargas = double.Parse(cargasBdv.Text) + double.Parse(cargasOtras.Text);

                double capacidadDePago = (ingresos * 0.2) - cargas;  // porcentaje de endudamineto fijado en 0.2 o 20% ...............###########################

                double ofiRiesgoso;
                if (oficioRiesgoso.IsChecked == true)  // SE PECHA CON 20% POR TENER UN OFICIO RIESGOSO
                {
                    ofiRiesgoso = 0.80;
                }
                else
                {
                    ofiRiesgoso = 1;
                }

                double montoAprobado = (capacidadDePago / 0.92162334) * 4 * ofiRiesgoso;
                double montoAprobadoSocial = (capacidadDePago / 0.3422002) * 4 * ofiRiesgoso;

                montoAprobado = Math.Round(montoAprobado / 50000) * 50000;

                if (listaProducto.Items.Count > 0)
                {
                    for (int i = 0; i < listaProducto.Items.Count; i++)
                    {
                        if (listaProducto.Items[i].ToString() == "TDC")
                        {
                            contador++; //ESTE CONTADOR INDICA CUANTAS TDC TIENE ········#########################
                        }
                    }
                }

                if (montoAprobado > 0)
                {
                    switch (contador)
                    {
                        case 0:
                            MessageBox.Show("No Posee TDC");
                            break;
                        case 2:
                            montoAprobado = Math.Round((montoAprobado / 2) / 50000) * 50000;  // multiplos de 500000 ········#########################
                            break;
                        case 3:
                            montoAprobado = Math.Round((montoAprobado / 3) / 50000) * 50000;  // multiplos de 500000 ········#########################
                            break;
                    }
                }
                else
                {
                    montoAprobado = 0;
                }

                if (montoAprobado < 1000000 && casoEspecial.IsChecked == false)
                {
                    riesgoConsolidado.Text = "BAJA CAPACIDAD DE PAGO"; // SE VERFICA QUE LE MONTO APROBADO SEA MAYOR A 1.000.000 SIEMPRE QUE NO SEA UN CASO ESPECIAL
                    riesgoConsolidado.Background = Brushes.Red;
                }
                else
                {
                    if (montoAprobado == 0) // SI ES UN CASO ESPECIAL PERO IGUAL ESTA NEGADO
                    {
                        riesgoConsolidado.Text = "BAJA CAPACIDAD DE PAGO"; 
                        riesgoConsolidado.Background = Brushes.Red;
                    }
                    else
                    {
                        riesgoConsolidado.Text = "Bs. " + (montoAprobado * contador).ToString("N2");
                        riesgoConsolidado.Background = Brushes.Green;

                        if (contador > 0)
                        {
                            for (int i = 0; i < contador; i++)
                            {
                                listaLimiteOtorgado.Items.Add("Bs." + montoAprobado.ToString("N2"));
                            }
                        }
                    }  
                }

                if (montoAprobadoSocial < 40000)
                {
                    montoAprobadoSocial = 0;
                }
                else
                {
                    montoAprobadoSocial = 40000;

                    contador = 0; // SE REINICIA EL VALOR DEL CONTADOR

                    if (listaProducto.Items.Count > 0)
                    {
                        for (int i = 0; i < listaProducto.Items.Count; i++)
                        {
                            if (listaProducto.Items[i].ToString() == "TDC_SOCIAL")
                            {
                                contador++; //ESTE CONTADOR INDICA CUANTAS TDC SOCIALES TIENE ········#########################
                            }
                        }
                    }

                    if (contador > 0)
                    {
                        for (int i = 0; i < contador; i++)
                        {
                            listaLimiteOtorgado.Items.Add("Bs." + montoAprobadoSocial.ToString("N2"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error en calculo, posibles argumentos invalidos");
            }            
        }

        private void CasoReferido_Click(object sender, RoutedEventArgs e)
        {
            if (casoReferido.IsChecked == true)
            {
                aprobadoreferido.IsEnabled = true;
            }
            else
            {
                aprobadoreferido.Text = "";
                aprobadoreferido.IsEnabled = false;
            }
        }

        private void Aprobadoreferido_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            decimal value = 0M;

            if (Decimal.TryParse(tb.Text, out value))
                tb.Text = "Bs. " + value.ToString("#,##0.00");
        }
    }
}


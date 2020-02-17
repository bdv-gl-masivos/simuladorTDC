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
using System.Globalization;

namespace WPFUI.Views
{
    public partial class ShellView : Window
    {
        List<ConsolidadoAcrClientModel> acr = new List<ConsolidadoAcrClientModel>();
        List<ConsolidadoAcrModel> clientes = new List<ConsolidadoAcrModel>();
        List<SaldosTdcModel> saldo = new List<SaldosTdcModel>();
        List<NominaBdvModel> empleado = new List<NominaBdvModel>();

        public ShellView()
        {
            InitializeComponent();
            nacionalidad.Items.Add("V");
            nacionalidad.Items.Add("E");
            nacionalidad.Items.Add("P");
            nacionalidad.Text = "V";
            CALIFICA.Text = "";
            USER.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            nroTarjeta.Items.Add("1");
            nroTarjeta.Items.Add("2");
            nroTarjeta.Text = "2";

            for (int i = 1; i < 32; i++)
            {
                dia.Items.Add(i.ToString());
            }
            dia.Text = "29";

            for (int i = 1; i < 13; i++)
            {
                mes.Items.Add(i.ToString());
            }
            mes.Text = "3";

            for (int i = 1920; i < 2010; i++)
            {
                año.Items.Add(i.ToString());
            }
            año.Text = "1990";

            ingresosOtros.Text = "Bs. 0,00";
            cargasBdv.Text = "Bs. 0,00";
            cargasOtras.Text = "Bs. 0,00";

        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar_Click(sender, e);
            calcular.IsEnabled = true;
            string cedu;
            
            cedu = "000000000" + cedulaCliente.Text;
            cedu = nacionalidad.Text + cedu.Substring(cedu.Length - 8);
            
            try
            {
                if (casoEmpleado.IsChecked == true)
                {
                    calcular.IsEnabled = false;
                    oficioRiesgoso.IsEnabled = false;
                    casoEspecial.IsEnabled = false;
                    casoReferido.IsEnabled = false;
                    ingresosOtros.IsEnabled = false;
                    cargasOtras.IsEnabled = false;


                    #region visibilidad_no

                    textCargo.Visibility = Visibility.Visible;
                    textNombre.Visibility = Visibility.Visible;
                    textNroTdc.Visibility = Visibility.Visible;
                    textTdcLimAct.Visibility = Visibility.Visible;
                    textTdcLimAsig.Visibility = Visibility.Visible;
                    listaCargo.Visibility = Visibility.Visible;
                    listaNombre.Visibility = Visibility.Visible;
                    listaTdcEmp.Visibility = Visibility.Visible;
                    listaTdcLimite.Visibility = Visibility.Visible;
                    listaTdcLimiteAsig.Visibility = Visibility.Visible;

                    text1.Visibility = Visibility.Collapsed;
                    text2.Visibility = Visibility.Collapsed;
                    text3.Visibility = Visibility.Collapsed;
                    text4.Visibility = Visibility.Collapsed;
                    text5.Visibility = Visibility.Collapsed;
                    text6.Visibility = Visibility.Collapsed;
                    text7.Visibility = Visibility.Collapsed;
                    text8.Visibility = Visibility.Collapsed;
                    text9.Visibility = Visibility.Collapsed;
                    text10.Visibility = Visibility.Collapsed;
                    text11.Visibility = Visibility.Collapsed;
                    text12.Visibility = Visibility.Collapsed;
                    text13.Visibility = Visibility.Collapsed;
                    text14.Visibility = Visibility.Collapsed;
                    text15.Visibility = Visibility.Collapsed;
                    text16.Visibility = Visibility.Collapsed;
                    text17.Visibility = Visibility.Collapsed;
                    text18.Visibility = Visibility.Collapsed;
                    text19.Visibility = Visibility.Collapsed;
                    text20.Visibility = Visibility.Collapsed;
                    text21.Visibility = Visibility.Collapsed;
                    text22.Visibility = Visibility.Collapsed;
                    text23.Visibility = Visibility.Collapsed;
                    text24.Visibility = Visibility.Collapsed;
                    text25.Visibility = Visibility.Collapsed;

                    listaProducto.Visibility = Visibility.Collapsed;
                    listaNroCredito.Visibility = Visibility.Collapsed;
                    listaHistorico.Visibility = Visibility.Collapsed;
                    listaMontoLiquidado.Visibility = Visibility.Collapsed;
                    listaFechaLiquidado.Visibility = Visibility.Collapsed;
                    listaFechaAumento.Visibility = Visibility.Collapsed;
                    listaMora.Visibility = Visibility.Collapsed;

                    listaMes_1.Visibility = Visibility.Collapsed;
                    listaMes_2.Visibility = Visibility.Collapsed;
                    listaMes_3.Visibility = Visibility.Collapsed;
                    listaSalario.Visibility = Visibility.Collapsed;
                    listaNombreEmpresa.Visibility = Visibility.Collapsed;
                    listaRifEmpresa.Visibility = Visibility.Collapsed;

                    listaMes_1_saldo.Visibility = Visibility.Collapsed;
                    listaMes_2_saldo.Visibility = Visibility.Collapsed;
                    listaMes_3_saldo.Visibility = Visibility.Collapsed;
                    listaSaldoPro.Visibility = Visibility.Collapsed;
                    clienteNombre.Visibility = Visibility.Collapsed;

                    cargasBdv.Visibility = Visibility.Collapsed;
                    cargasOtras.Visibility = Visibility.Collapsed;

                    ingresosOtros.Visibility = Visibility.Collapsed;
                    calcular.Visibility = Visibility.Collapsed;

                    riesgoConsolidado.Visibility = Visibility.Collapsed;

                    listaTarjetas.Visibility = Visibility.Collapsed;
                    listaLimite.Visibility = Visibility.Collapsed;
                    listaLimiteOtorgado.Visibility = Visibility.Collapsed;
                    aprobadoreferido.Visibility = Visibility.Collapsed;

                    #endregion

                    empleado = SqliteDataAccess.CargaCedulaEmp(cedu);
                    clientes = SqliteDataAccess.CargaCedula(cedu);

                    listaCargo.Items.Add(empleado[0].CARGO.ToString());
                    listaNombre.Items.Add(empleado[0].NOMBRE.ToString());
        

                    for (int i = 0; i < clientes.Count; i++) // SE AGREGA LISTADO DE NRO DE TARJETAS DEL EMPLEADO
                    {

                        if (clientes[i].PRODUCTO.ToString() == "TDC")
                        {
                            listaTdcEmp.Items.Add(clientes[i].NUMERO_CREDITO.ToString());
                        }
                    }

                    for (int i = 0; i < clientes.Count; i++) // SE AGREGA LISTADO DE LIMITE DE TARJETAS DEL EMPLEADO
                    {

                        if (clientes[i].PRODUCTO.ToString() == "TDC")
                        {
                            listaTdcLimite.Items.Add("Bs. " + clientes[i].MONTO_LIQUIDADO.ToString("N2"));
                        }
                    }

                    for (int i = 0; i < 2; i++) // SE AGREGA LISTADO DE LIMITE !!!ASIGNADO DE TARJETAS DEL EMPLEADO SEGUN NIVEL
                    {
                        listaTdcLimiteAsig.Items.Add("Bs. " + empleado[0].LIMITE_TDC.ToString("N2"));                
                    }


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

                    return;

                }
                else
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

                        for (int i = 0; i < clientes.Count; i++) // SE AGREGA LOS PROCTOS SOCIALES AL FINAL DE LA LISTA
                        {

                            if (clientes[i].PRODUCTO.ToString() == "TDC_SOCIAL")
                            {
                                listaTarjetas.Items.Add(clientes[i].NUMERO_CREDITO.ToString());
                                listaLimite.Items.Add(clientes[i].MONTO_LIQUIDADO.ToString("N2"));
                            }
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

                    cargasBdv.Text = "Bs. " + acr[0].CARGA_BDV.ToString("N2");
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("NO POSEE EXPERIENCIA BDV");
                CALIFICA.Text = "CALIFICA";
                CALIFICA.Background = Brushes.Green;
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

                clienteNombre.Text = saldo[0].NOMBRE.ToString();
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
        }// CONTROLADOR  TECLA ENTER SOBRE EL CAMPO DE CEDULA EJECUTA LA BUSQUEDA

        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {

            text26.Visibility = Visibility.Collapsed;
            riesgoConsolidadoImp.Text = "";
            riesgoConsolidadoImp.Visibility = Visibility.Collapsed;

            clienteNombreImp.Text = "";
            clienteNombreImp.Visibility = Visibility.Collapsed;

            logoBdv.Visibility = Visibility.Visible;
            buscar.Visibility = Visibility.Visible;
            optionStack.Visibility = Visibility.Visible;
            ivvsConsulta.Visibility = Visibility.Visible;
            optionStack2.Visibility = Visibility.Visible;
            USER.Visibility = Visibility.Visible;
            optionStack3.Visibility = Visibility.Visible;

            observacionImp.Visibility = Visibility.Collapsed;
            observacionTextImp.Visibility = Visibility.Collapsed;

            referidoPorImp.Visibility = Visibility.Collapsed;
            referidoPorTextImp.Visibility = Visibility.Collapsed;

            observacionImp.Text = "";
            referidoPorImp.Text = "";

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
            clienteNombre.Text = "";

            dia.Text = "";
            mes.Text = "";
            año.Text = "";

            listaCargo.Items.Clear();
            listaNombre.Items.Clear();
            listaTdcEmp.Items.Clear();
            listaTdcLimite.Items.Clear();
            listaTdcLimiteAsig.Items.Clear();

            calcular.IsEnabled = true;
            oficioRiesgoso.IsEnabled = true;
            casoEspecial.IsEnabled = true;
            casoReferido.IsEnabled = true;
            ingresosOtros.IsEnabled = true;
            cargasOtras.IsEnabled = true;

            aprobadoreferido.IsEnabled = false;

            observacion.Text = "";

            referidoPor.IsEnabled = false;
            referidoPor.Text = "";

            archivar_TDC.IsEnabled = true;

            #region visible_si

            textCargo.Visibility = Visibility.Collapsed;
            textNombre.Visibility = Visibility.Collapsed;
            textNroTdc.Visibility = Visibility.Collapsed;
            textTdcLimAct.Visibility = Visibility.Collapsed;
            textTdcLimAsig.Visibility = Visibility.Collapsed;
            listaCargo.Visibility = Visibility.Collapsed;
            listaNombre.Visibility = Visibility.Collapsed;
            listaTdcEmp.Visibility = Visibility.Collapsed;
            listaTdcLimite.Visibility = Visibility.Collapsed;
            listaTdcLimiteAsig.Visibility = Visibility.Collapsed;

            text1.Visibility = Visibility.Visible;
            text2.Visibility = Visibility.Visible;
            text3.Visibility = Visibility.Visible;
            text4.Visibility = Visibility.Visible;
            text5.Visibility = Visibility.Visible;
            text6.Visibility = Visibility.Visible;
            text7.Visibility = Visibility.Visible;
            text8.Visibility = Visibility.Visible;
            text9.Visibility = Visibility.Visible;
            text10.Visibility = Visibility.Visible;
            text11.Visibility = Visibility.Visible;
            text12.Visibility = Visibility.Visible;
            text13.Visibility = Visibility.Visible;
            text14.Visibility = Visibility.Visible;
            text15.Visibility = Visibility.Visible;
            text16.Visibility = Visibility.Visible;
            text17.Visibility = Visibility.Visible;
            text18.Visibility = Visibility.Visible;
            text19.Visibility = Visibility.Visible;
            text20.Visibility = Visibility.Visible;
            text21.Visibility = Visibility.Visible;
            text22.Visibility = Visibility.Visible;
            text23.Visibility = Visibility.Visible;
            text24.Visibility = Visibility.Visible;
            text25.Visibility = Visibility.Visible;

            listaProducto.Visibility = Visibility.Visible;
            listaNroCredito.Visibility = Visibility.Visible;
            listaHistorico.Visibility = Visibility.Visible;
            listaMontoLiquidado.Visibility = Visibility.Visible;
            listaFechaLiquidado.Visibility = Visibility.Visible;
            listaFechaAumento.Visibility = Visibility.Visible;
            listaMora.Visibility = Visibility.Visible;

            listaMes_1.Visibility = Visibility.Visible;
            listaMes_2.Visibility = Visibility.Visible;
            listaMes_3.Visibility = Visibility.Visible;
            listaSalario.Visibility = Visibility.Visible;
            listaNombreEmpresa.Visibility = Visibility.Visible;
            listaRifEmpresa.Visibility = Visibility.Visible;

            listaMes_1_saldo.Visibility = Visibility.Visible;
            listaMes_2_saldo.Visibility = Visibility.Visible;
            listaMes_3_saldo.Visibility = Visibility.Visible;
            listaSaldoPro.Visibility = Visibility.Visible;
            clienteNombre.Visibility = Visibility.Visible;

            cargasBdv.Visibility = Visibility.Visible;
            cargasOtras.Visibility = Visibility.Visible;

            ingresosOtros.Visibility = Visibility.Visible;
            calcular.Visibility = Visibility.Visible;

            riesgoConsolidado.Visibility = Visibility.Visible;

            listaTarjetas.Visibility = Visibility.Visible;
            listaLimite.Visibility = Visibility.Visible;
            listaLimiteOtorgado.Visibility = Visibility.Visible;
            aprobadoreferido.Visibility = Visibility.Visible;

            #endregion

            nroTarjetaText.Visibility = Visibility.Collapsed;
            nroTarjeta.Visibility = Visibility.Collapsed;
            nroTarjeta.Text = "2";

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
                    cargasBdv.Text = "Bs. 0,00";
                }

                if (ingresosOtros.Text == "")
                {
                    ingresosOtros.Text = "Bs. 0,00";
                }

                if (cargasOtras.Text == "")
                {
                    cargasOtras.Text = "Bs. 0,00";
                }

                
                double ingresos = double.Parse(ingresosOtros.Text.Substring(3, ingresosOtros.Text.Length - 3));
                double cargas = double.Parse(cargasBdv.Text.Substring(3, cargasBdv.Text.Length - 3)) + double.Parse(cargasOtras.Text.Substring(3, cargasOtras.Text.Length - 3));

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

                double montoAprobado = 0;
                double montoAprobadoSocial = 0;
                montoAprobado = (capacidadDePago / 0.92162334) * 4 * ofiRiesgoso;
                montoAprobadoSocial = (capacidadDePago / 0.3422002) * 4 * ofiRiesgoso;

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

                if (montoAprobado > 0 && !listaProducto.Items.Contains("TDC_SOCIAL"))
                {
                    switch (contador)
                    {
                        case 0:
                            MessageBox.Show("No Posee TDC");
                            montoAprobado = Math.Round((montoAprobado / 1) / 50000) * 50000;
                            if (montoAprobado > 10000000) // SE FIJA MONTO MAXIMO
                            {
                                montoAprobado = 10000000;
                            }
                            contador = 1;
                            break;
                        case 2:
                            montoAprobado = Math.Round((montoAprobado / 2) / 50000) * 50000;  // multiplos de 500000 ········#########################
                            if (montoAprobado > 10000000) // SE FIJA MONTO MAXIMO
                            {
                                montoAprobado = 10000000;
                            }
                            break;
                        case 3:
                            montoAprobado = Math.Round((montoAprobado / 3) / 50000) * 50000;  // multiplos de 500000 ········#########################
                            if (montoAprobado > 10000000) // SE FIJA MONTO MAXIMO
                            {
                                montoAprobado = 10000000;
                            }
                            break;
                    }
                }
                else
                {
                    montoAprobado = 0;
                }

                if (montoAprobado < 1000000 && casoEspecial.IsChecked == false && !listaProducto.Items.Contains("TDC_SOCIAL"))
                {
                    riesgoConsolidado.Text = "BAJA CAPACIDAD DE PAGO"; // SE VERFICA QUE LE MONTO APROBADO SEA MAYOR A 1.000.000 SIEMPRE QUE NO SEA UN CASO ESPECIAL
                    riesgoConsolidado.Background = Brushes.Red;
                }
                else
                {
                    if (montoAprobado == 0 && !listaProducto.Items.Contains("TDC_SOCIAL")) // SI ES UN CASO ESPECIAL PERO IGUAL ESTA NEGADO
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

            listaLimiteOtorgado.Items.Clear();

            if (calcular.IsEnabled == true)
            {
                calcular.IsEnabled = false;
            }
            else
            {
                calcular.IsEnabled = true;
            }

            if (referidoPor.IsEnabled == true)
            {
                referidoPor.IsEnabled = false;
                referidoPor.Text = "";
            }
            else
            {
                referidoPor.IsEnabled = true;
            }


            if (casoReferido.IsChecked == true)
            {
                aprobadoreferido.IsEnabled = true;
            }
            else
            {
                aprobadoreferido.Text = "";
                aprobadoreferido.IsEnabled = false;
            }

            if (casoReferido.IsChecked == true)
            {
                nroTarjetaText.Visibility = Visibility.Visible;
                nroTarjeta.Visibility = Visibility.Visible;
                nroTarjeta.Text = "2";
            }
            else
            {
                nroTarjetaText.Visibility = Visibility.Collapsed;
                nroTarjeta.Visibility = Visibility.Collapsed;
                nroTarjeta.Text = "2";

            }

            cargasOtras.Text = "Bs. 0,00";
            ingresosOtros.Text = "Bs. 0,00";
            cargasBdv.Text = "Bs. 0,00";

        }

        private void Aprobadoreferido_LostFocus(object sender, RoutedEventArgs e)
        {
            
            listaLimiteOtorgado.Items.Clear();
            try
            {
                
                double aprobadoRef = double.Parse(aprobadoreferido.Text);

                if (listaTarjetas.Items.Count > 0)
                {
                    for (int i = 0; i < int.Parse(nroTarjeta.Text); i++)
                    {
                        listaLimiteOtorgado.Items.Add("Bs. " + (aprobadoRef).ToString("N2"));
                    }
                }
                
                aprobadoRef = aprobadoRef * int.Parse(nroTarjeta.Text);

                aprobadoreferido.Text = aprobadoRef.ToString();

                TextBox tb = (sender as TextBox);
                decimal value = 0M;

                if (Decimal.TryParse(tb.Text, out value))
                    tb.Text = "Bs. " + value.ToString("#,##0.00");

            }
            catch (Exception)
            {

            }
        }

        private void IngresosOtros_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            decimal value = 0M;

            if (Decimal.TryParse(tb.Text, out value))
                tb.Text = "Bs. " + value.ToString("#,##0.00");
        }// MODIFICA EL FORMATO UNA VEZ PIERDE EL FOCO LA CELDA

        private void CargasOtras_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            decimal value = 0M;

            if (Decimal.TryParse(tb.Text, out value))
                tb.Text = "Bs. " + value.ToString("#,##0.00");
        }// MODIFICA EL FORMATO UNA VEZ PIERDE EL FOCO LA CELDA

        private void IvvsConsulta_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ivss.gob.ve:28083/CuentaIndividualIntranet/CtaIndividual_PortalCTRL?nacionalidad_aseg=" + nacionalidad.Text + "&cedula_aseg=" + cedulaCliente.Text + "&d=" + dia.Text + "&m=" + mes.Text + "&y=" + año.Text + "");

            
        } // CONSULTA EN LA PAGINA DEL IVVS

        private void IMPRIMIR_Click(object sender, RoutedEventArgs e)
        {
            logoBdv.Visibility = Visibility.Hidden;

            buscar.Visibility = Visibility.Collapsed;
            optionStack.Visibility = Visibility.Collapsed;
            ivvsConsulta.Visibility = Visibility.Collapsed;
            optionStack2.Visibility = Visibility.Collapsed;
            USER.Visibility = Visibility.Collapsed;
            optionStack3.Visibility = Visibility.Collapsed;

            text1.Visibility = Visibility.Collapsed;
            text2.Visibility = Visibility.Collapsed;
            text3.Visibility = Visibility.Collapsed;
            text4.Visibility = Visibility.Collapsed;
            text5.Visibility = Visibility.Collapsed;
            text6.Visibility = Visibility.Collapsed;
            text7.Visibility = Visibility.Collapsed;
            text8.Visibility = Visibility.Collapsed;
            text9.Visibility = Visibility.Collapsed;
            text10.Visibility = Visibility.Collapsed;
            text11.Visibility = Visibility.Collapsed;
            text12.Visibility = Visibility.Collapsed;
            text13.Visibility = Visibility.Collapsed;
            text14.Visibility = Visibility.Collapsed;
            text15.Visibility = Visibility.Collapsed;
            text16.Visibility = Visibility.Collapsed;
            text17.Visibility = Visibility.Collapsed;
            text18.Visibility = Visibility.Collapsed;
                      
            text21.Visibility = Visibility.Collapsed;


            listaProducto.Visibility = Visibility.Collapsed;
            listaNroCredito.Visibility = Visibility.Collapsed;
            listaHistorico.Visibility = Visibility.Collapsed;
            listaMontoLiquidado.Visibility = Visibility.Collapsed;
            listaFechaLiquidado.Visibility = Visibility.Collapsed;
            listaFechaAumento.Visibility = Visibility.Collapsed;
            listaMora.Visibility = Visibility.Collapsed;

            listaMes_1.Visibility = Visibility.Collapsed;
            listaMes_2.Visibility = Visibility.Collapsed;
            listaMes_3.Visibility = Visibility.Collapsed;
            listaSalario.Visibility = Visibility.Collapsed;
            listaNombreEmpresa.Visibility = Visibility.Collapsed;
            listaRifEmpresa.Visibility = Visibility.Collapsed;

            listaMes_1_saldo.Visibility = Visibility.Collapsed;
            listaMes_2_saldo.Visibility = Visibility.Collapsed;
            listaMes_3_saldo.Visibility = Visibility.Collapsed;
            listaSaldoPro.Visibility = Visibility.Collapsed;

            clienteNombreImp.Text = clienteNombre.Text;
            clienteNombreImp.Visibility = Visibility.Visible;
            clienteNombre.Visibility = Visibility.Collapsed;

            riesgoConsolidadoImp.Visibility = Visibility.Visible;
            aprobadoreferido.Visibility = Visibility.Collapsed;
            
            text26.Visibility = Visibility.Visible;

            if (casoReferido.IsChecked == true)
            {
                riesgoConsolidadoImp.Text = aprobadoreferido.Text;
                riesgoConsolidadoImp.Foreground = Brushes.White;
            }
            else
            {
                riesgoConsolidadoImp.Text = riesgoConsolidado.Text;
                riesgoConsolidadoImp.Foreground = Brushes.White;
            }

            
            riesgoConsolidado.Visibility = Visibility.Collapsed;

            observacion.Visibility = Visibility.Collapsed;
            observacionText.Visibility = Visibility.Collapsed;

            referidoPor.Visibility = Visibility.Collapsed;
            referidoPorText.Visibility = Visibility.Collapsed;

            observacionTextImp.Text = observacionText.Text;
            observacionImp.Text = observacion.Text;

            referidoPorTextImp.Text = referidoPorText.Text;
            referidoPorImp.Text = referidoPor.Text;

            observacionTextImp.Visibility = Visibility.Visible;
            observacionImp.Visibility = Visibility.Visible;

            referidoPorTextImp.Visibility = Visibility.Visible;
            referidoPorImp.Visibility = Visibility.Visible;

            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();

            if (printDlg.ShowDialog() == true)

            {
                //get selected printer capabilities

                System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

                //get scale of the print wrt to screen of WPF visual

                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / this.ActualWidth, capabilities.PageImageableArea.ExtentHeight /

                               this.ActualHeight);

                //Transform the Visual to scale

                this.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page

                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.

                this.Measure(sz);

                this.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                //now print the visual to printer to fit on the one page.

                printDlg.PrintVisual(this, "First Fit to Page WPF Print");

            }
        } // IMPRPIME VENTANA ACTIVA

        private void Archivar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NM = USER.Text.Substring(9);
                string CEDULA = nacionalidad.Text + ("000000000" + cedulaCliente.Text).Substring(("000000000" + cedulaCliente.Text).Length - 8);
                string FECHA_HORA = System.DateTime.Now.ToString();

                if (casoEmpleado.IsChecked == false) // NO ES EMPLEADO
                {

                    double LIMITE_CONSOLIDADO;
                    string NOMBRE = clienteNombre.Text;
                    string REFERIDO;

                    if (casoReferido.IsChecked == true)
                    {
                        LIMITE_CONSOLIDADO = double.Parse(aprobadoreferido.Text.Substring(3, aprobadoreferido.Text.Length - 3));
                        REFERIDO = "SI";
                    }
                    else
                    {
                        LIMITE_CONSOLIDADO = double.Parse(riesgoConsolidado.Text.Substring(3, riesgoConsolidado.Text.Length - 3));
                        REFERIDO = "NO";
                    }

                    string OBSERVACION = observacion.Text;

                    string EMPLEADO;

                    if (casoEmpleado.IsChecked == true)
                    {
                        EMPLEADO = "SI";
                    }
                    else
                    {
                        EMPLEADO = "NO";
                    }

                    string REFERIDO_POR;
                    if (casoReferido.IsChecked == true)
                    {
                        REFERIDO_POR = referidoPor.Text;
                    }
                    else
                    {
                        REFERIDO_POR = "NA";
                    }

                    string CARGAS_BDV = ((cargasBdv.Text.Substring(3, cargasBdv.Text.Length - 3)).Replace(".", "")).Replace(",", ".");

                    string INGRESOS = ((ingresosOtros.Text.Substring(3, ingresosOtros.Text.Length - 3)).Replace(".", "")).Replace(",", ".");

                    string OTRAS_CARGAS = ((cargasOtras.Text.Substring(3, cargasOtras.Text.Length - 3)).Replace(".", "")).Replace(",", ".");

                    double LIMITE_1 = 0;
                    double LIMITE_2 = 0;
                    double LIMITE_3 = 0;
                    double LIMITE_4 = 0;

                    if (listaLimiteOtorgado.Items.Count <= 1)
                    {
                        LIMITE_1 = double.Parse((listaLimiteOtorgado.Items[0].ToString()).Substring(3, (listaLimiteOtorgado.Items[0].ToString().Length) - 3));
                    }

                    if (listaLimiteOtorgado.Items.Count == 2)
                    {
                        LIMITE_1 = double.Parse((listaLimiteOtorgado.Items[0].ToString()).Substring(3, (listaLimiteOtorgado.Items[0].ToString().Length) - 3));
                        LIMITE_2 = double.Parse((listaLimiteOtorgado.Items[1].ToString()).Substring(3, (listaLimiteOtorgado.Items[1].ToString().Length) - 3));
                    }

                    if (listaLimiteOtorgado.Items.Count == 3)
                    {
                        LIMITE_1 = double.Parse((listaLimiteOtorgado.Items[0].ToString()).Substring(3, (listaLimiteOtorgado.Items[0].ToString().Length) - 3));
                        LIMITE_2 = double.Parse((listaLimiteOtorgado.Items[1].ToString()).Substring(3, (listaLimiteOtorgado.Items[1].ToString().Length) - 3));
                        LIMITE_3 = double.Parse((listaLimiteOtorgado.Items[2].ToString()).Substring(3, (listaLimiteOtorgado.Items[2].ToString().Length) - 3));
                    }

                    if (listaLimiteOtorgado.Items.Count == 4)
                    {
                        LIMITE_1 = double.Parse((listaLimiteOtorgado.Items[0].ToString()).Substring(3, (listaLimiteOtorgado.Items[0].ToString().Length) - 3));
                        LIMITE_2 = double.Parse((listaLimiteOtorgado.Items[1].ToString()).Substring(3, (listaLimiteOtorgado.Items[1].ToString().Length) - 3));
                        LIMITE_3 = double.Parse((listaLimiteOtorgado.Items[2].ToString()).Substring(3, (listaLimiteOtorgado.Items[2].ToString().Length) - 3));
                        LIMITE_4 = double.Parse((listaLimiteOtorgado.Items[3].ToString()).Substring(3, (listaLimiteOtorgado.Items[3].ToString().Length) - 3));
                    }

                    string NRO_TDC_1 = "NA";
                    string NRO_TDC_2 = "NA";
                    string NRO_TDC_3 = "NA";
                    string NRO_TDC_4 = "NA";

                    if (listaTarjetas.Items.Count == 1)
                    {
                        NRO_TDC_1 = listaTarjetas.Items[0].ToString();
                    }

                    if (listaTarjetas.Items.Count == 2)
                    {
                        NRO_TDC_1 = listaTarjetas.Items[0].ToString();
                        NRO_TDC_2 = listaTarjetas.Items[1].ToString();
                    }

                    if (listaTarjetas.Items.Count == 3)
                    {
                        NRO_TDC_1 = listaTarjetas.Items[0].ToString();
                        NRO_TDC_2 = listaTarjetas.Items[1].ToString();
                        NRO_TDC_3 = listaTarjetas.Items[2].ToString();
                    }

                    if (listaTarjetas.Items.Count == 4)
                    {
                        NRO_TDC_1 = listaTarjetas.Items[0].ToString();
                        NRO_TDC_2 = listaTarjetas.Items[1].ToString();
                        NRO_TDC_3 = listaTarjetas.Items[2].ToString();
                        NRO_TDC_4 = listaTarjetas.Items[3].ToString();
                    }

                    string CALIFICA_ARCHIVO = CALIFICA.Text;

                    SqliteDataAccess.Archivar(NM, CEDULA, NOMBRE, FECHA_HORA,
                    LIMITE_CONSOLIDADO, REFERIDO, OBSERVACION, EMPLEADO,
                    REFERIDO_POR, CARGAS_BDV, INGRESOS, OTRAS_CARGAS,
                    LIMITE_1, LIMITE_2, LIMITE_3, LIMITE_4, NRO_TDC_1,
                    NRO_TDC_2, NRO_TDC_3, NRO_TDC_4, CALIFICA_ARCHIVO);

                    MessageBox.Show("REGISTRO ARCHIVADO EXITOSAMENTE");

                    archivar_TDC.IsEnabled = false;

                }
                else // ES EMPLEADO BDV
                {
 
                    double LIMITE_1 = 0;
                    double LIMITE_2 = 0;
                    double LIMITE_3 = 0;
                    double LIMITE_4 = 0;

                    if (listaTdcLimiteAsig.Items.Count <= 1)
                    {
                        LIMITE_1 = double.Parse((listaTdcLimiteAsig.Items[0].ToString()).Substring(3, (listaTdcLimiteAsig.Items[0].ToString().Length) - 3));
                    }

                    if (listaTdcLimiteAsig.Items.Count == 2)
                    {
                        LIMITE_1 = double.Parse((listaTdcLimiteAsig.Items[0].ToString()).Substring(3, (listaTdcLimiteAsig.Items[0].ToString().Length) - 3));
                        LIMITE_2 = double.Parse((listaTdcLimiteAsig.Items[1].ToString()).Substring(3, (listaTdcLimiteAsig.Items[1].ToString().Length) - 3));
                    }

                    if (listaTdcLimiteAsig.Items.Count == 3)
                    {
                        LIMITE_1 = double.Parse((listaTdcLimiteAsig.Items[0].ToString()).Substring(3, (listaTdcLimiteAsig.Items[0].ToString().Length) - 3));
                        LIMITE_2 = double.Parse((listaTdcLimiteAsig.Items[1].ToString()).Substring(3, (listaTdcLimiteAsig.Items[1].ToString().Length) - 3));
                        LIMITE_3 = double.Parse((listaTdcLimiteAsig.Items[2].ToString()).Substring(3, (listaTdcLimiteAsig.Items[2].ToString().Length) - 3));
                    }

                    double LIMITE_CONSOLIDADO = LIMITE_1 + LIMITE_2 + LIMITE_3 + LIMITE_4;                   
                    
                    string CARGAS_BDV = "0";

                    string INGRESOS = "0";

                    string OTRAS_CARGAS = "0";

                    string REFERIDO_POR = "NA";

                    string REFERIDO = "NA";

                    string OBSERVACION = "NA";

                    string EMPLEADO = "SI";

                    string NRO_TDC_1 = "NA";
                    string NRO_TDC_2 = "NA";
                    string NRO_TDC_3 = "NA";
                    string NRO_TDC_4 = "NA";

                    if (listaTdcEmp.Items.Count == 1)
                    {
                        NRO_TDC_1 = listaTdcEmp.Items[0].ToString();
                    }

                    if (listaTdcEmp.Items.Count == 2)
                    {
                        NRO_TDC_1 = listaTdcEmp.Items[0].ToString();
                        NRO_TDC_2 = listaTdcEmp.Items[1].ToString();
                    }

                    if (listaTdcEmp.Items.Count == 3)
                    {
                        NRO_TDC_1 = listaTdcEmp.Items[0].ToString();
                        NRO_TDC_2 = listaTdcEmp.Items[1].ToString();
                        NRO_TDC_3 = listaTdcEmp.Items[2].ToString();
                    }

                    string CALIFICA_ARCHIVO = CALIFICA.Text;

                    string NOMBRE = listaNombre.Items[0].ToString().Replace(",", " ");

                   
                    SqliteDataAccess.Archivar(NM, CEDULA, NOMBRE, FECHA_HORA,
                    LIMITE_CONSOLIDADO, REFERIDO, OBSERVACION, EMPLEADO,
                    REFERIDO_POR, CARGAS_BDV, INGRESOS, OTRAS_CARGAS,
                    LIMITE_1, LIMITE_2, LIMITE_3, LIMITE_4, NRO_TDC_1,
                    NRO_TDC_2, NRO_TDC_3, NRO_TDC_4, CALIFICA_ARCHIVO);

                    MessageBox.Show("REGISTRO ARCHIVADO EXITOSAMENTE");

                    archivar_TDC.IsEnabled = false;

                }

            }
            catch (Exception p)
            {
                MessageBox.Show("ERROR EN ARCHIVADO");
                MessageBox.Show(p.ToString());

            }

        } // FUNCION DE BOTON DE ARCHIVADO

        private void CasoEmpleado_Click(object sender, RoutedEventArgs e)
        {

            if (referidoPor.IsVisible == true)
            {
                referidoPor.Visibility = Visibility.Collapsed;
                observacion.Visibility = Visibility.Collapsed;
                referidoPorText.Visibility = Visibility.Collapsed;
                observacionText.Visibility = Visibility.Collapsed;

            }
            else
            {
                referidoPor.Visibility = Visibility.Visible;
                observacion.Visibility = Visibility.Visible;
                referidoPorText.Visibility = Visibility.Visible;
                observacionText.Visibility = Visibility.Visible;
            }
        }
    }
}


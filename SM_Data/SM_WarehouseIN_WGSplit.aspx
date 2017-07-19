﻿<%@ Page Title="入库单拆分" Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true" CodeBehind="SM_WarehouseIN_WGSplit.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_WGSplit" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>   


<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
        
        <script type="text/javascript" language="javascript">
        
        function checkSN(tb) {
       
            var sn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var rn = par.getElementsByTagName("td")[12].getElementsByTagName("span")[0].innerHTML;
            var quantity = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);
            var sq = par.getElementsByTagName("td")[15].getElementsByTagName("input")[0];
            var unitprice = parseFloat(par.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);
            var ctup = parseFloat(par.getElementsByTagName("td")[18].getElementsByTagName("span")[0].innerHTML);
            var samount = par.getElementsByTagName("td")[20].getElementsByTagName("input")[0];
            var scta = par.getElementsByTagName("td")[22].getElementsByTagName("input")[0]; 
            if (isNaN(sn)) {
                alert("请正确输入拆分数量！");
                tb.value = rn;
                sn = tb.value;
 //             sq.value = Math.round((sn / rn) * quantity); 
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
            if (sn > rn || sn <= 0 ) {
                alert("拆分数量必须大于0并且不大于实收数量！");
                tb.value = rn;
                sn = tb.value;
//              sq.value = Math.round((sn / rn) * quantity);
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
            if(sn.toFixed(4)==0&&sn>0)
            {
                alert("拆分数量必须是四位有效数字！");
                tb.value = rn;
                sn = tb.value;
//              sq.value = Math.round((sn / rn) * quantity);
                samount.value = (sn * unitprice).toFixed(2);
                scta.value = (sn * ctup).toFixed(2);
                Statistic();
                return;
            }
//            sq.value = Math.round((sn / rn) * quantity);
            samount.value = (sn * unitprice).toFixed(2);
            scta.value = (sn * ctup).toFixed(2);
            Statistic();     
        }
         
          function checkSN1(tb) {
         
            var par = tb.parentNode.parentNode;

//         总数

            var sq = parseFloat(tb.value);
              
            var quantity = parseFloat(par.childNodes[14].getElementsByTagName("span")[0].innerHTML);
            
//         拆分数量

            if (isNaN(sq)) {
                alert("请正确输入拆分张（支）数！");
                tb.value = quantity;
                return;
            }
            if (sq > quantity || sq <0 ) {
                alert("拆分张（支）数必须大于0并且不大于实收张（支）数！");
                tb.value =quantity;
                return;
            }
            Statistic(); 
        }
         
        function Statistic() 
        {
            var tsn = 0;
            var tsqn = 0;
            var tsa = 0;
            var tscta = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
             {
                 var val1 = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
                tsn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                tsqn += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                tsa += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                tscta += parseFloat(val4);
            }
            var lbtsn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[13].getElementsByTagName("span")[0];
            lbtsn.innerHTML = tsn.toFixed(4);
            var lbtsqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[15].getElementsByTagName("span")[0];
            lbtsqn.innerHTML =Math.round(tsqn);
            var lbtsa = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[20].getElementsByTagName("span")[0];
            lbtsa.innerHTML = tsa.toFixed(2);
            var lbtscta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[22].getElementsByTagName("span")[0];
            lbtscta.innerHTML = tscta.toFixed(2);
        }
        
        function ConfirmTip() {
            var retVal = confirm("确定进行拆分？");
            return retVal;
        }
           
        function CancelTip() {
            var retVal = confirm("放弃本次拆分并返回？");
            return retVal;
        }

        /*检查是否所有条目都被完全拆分，暂时不阻止这种情况 */
        function pagecheck() {
            var flag = false;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("span")[0].innerHTML;
                var val2 = gv1.rows[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value;
                if (val1 != val2) {
                    flag = true;
                }
            }
            if (flag == false) {
                alert("不能将数据完全拆分到子单！");
            }
            return flag;
        }     
    </script>
    
    
    
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" Width="300px"></asp:Label>
            </td>
            <td align="right" style="width:100%">
            <asp:Button ID="Confirm" runat="server" Text="确定" CausesValidation="False" OnClick="Confirm_Click" OnClientClick="ConfirmTip()"/>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Cancel" runat="server" Text="取消"  CausesValidation="False" OnClick="Cancel_Click" OnClientClick="return CancelTip()"/>&nbsp;&nbsp;&nbsp;
            </td>    
        </tr>
    </table>
    </div>
    </div>
    </div>
    
      
    <div class="box-wrapper">
    <div class="box-outer">
   
    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td style=" font-size:large; text-align:center;" colspan="3">
                外购入库单<asp:Image ID="ImageVerify1" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="False"/>&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;<asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false"/>&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="LabelHSState" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:Label ID="LabelDate" runat="server"></asp:Label>
            </td>
            <td>
                子单编号：<asp:Label ID="LabelNewCode" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                <asp:Label ID="LabelSupplierCode" runat="server" Visible="False"></asp:Label>
            </td> 
            <td>
                收料仓库：<asp:Label ID="LabelWarehouse" runat="server"></asp:Label>
                <asp:Label ID="LabelWarehouseCode" runat="server" Visible="false"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:Label ID="LabelAbstract" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td>
                货单编号：<asp:Label ID="LabelHDBH" runat="server"></asp:Label>
                
            </td> 
            <td>
              
            </td>
            <td>
               
            </td>
        </tr>
    </table>
    </asp:Panel>
    
    <asp:Panel ID="PanelBody" runat="server"  Style="margin:0 auto 0 auto" Width="99%" Height="300px">
    
    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" 
     ShowFooter="True" EmptyDataText="请追加需要的行数！" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                    </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                         <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>' Visible="false"></asp:Label>                         
                         <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelFoot" runat="server" Text="合计："></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>' ></asp:Label>
                    </ItemTemplate>
                    
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="型号规格" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false" >
                    <ItemTemplate>
                        <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false">        
                    <ItemTemplate >
                        <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>' ></asp:Label>
                       </ItemTemplate>
                       <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelUnit" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="实收数量" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拆分数量" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxSN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SN")%>' style="width: 60px"  onblur="checkSN(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalSN" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="张(支)数" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拆分张(支)数" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <input type="text" id="InputSQN"  runat="server" value='<%#DataBinder.Eval(Container.DataItem, "SQN")%>' style="width: 60px" onblur="checkSN1(this)"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalSQN" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单价" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="税率" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelTaxRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TaxRate")%>'></asp:Label>%
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税单价" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelCTUP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CTUP")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="金额" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelAmount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Amount")%>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalAmount" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拆分金额" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <input type="text" id="InputSAmount" value='<%#DataBinder.Eval(Container.DataItem, "SAmount")%>' style="width: 60px" readonly='readonly' runat="server"/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalSAmount" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="含税金额" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelCTA" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CTA")%>' ></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalCTA" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="拆分含税金额" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <input type="text" id="InputSCTA" value='<%#DataBinder.Eval(Container.DataItem, "SCTA")%>' style="width: 60px" readonly="readonly" runat="server"/>
                 </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalSCTA" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>' ></asp:Label>
                        <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LablePosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>' ></asp:Label>
                        <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>' ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="标识号" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelCGMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CGMODE")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
            </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
    <RowStyle BackColor="#EFF3FB" Wrap="false"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False"/>
    <EditRowStyle BackColor="#2461BF" />
    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4" />     
    </yyc:SmartGridView>
    </asp:Panel>
     
     <asp:Panel ID="FooterPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:Label ID="LabelDep" runat="server"></asp:Label>
                <asp:Label ID="LabelDepCode" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;业务员：<asp:Label ID="LabelClerk" runat="server"></asp:Label>
                <asp:Label ID="LabelClerkCode" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                <asp:Label ID="LabelDocCode" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;收料员：<asp:Label ID="LabelAcceptance" runat="server"></asp:Label>
                <asp:Label ID="LabelAcceptanceCode" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server"></asp:Label>
                <asp:Label ID="LabelVerifierCode" runat="server" Visible="False"></asp:Label>
            </td>                    
            <td>
                审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    </div>
    </div>
    
    
     <script type="text/javascript">  
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")  //点击某一行选中
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                
                var dataRow = sDataTable.tBodies[0].rows[i];
                
                return function () 
                      {
                            if (dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked) 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                dataRow.style.backgroundColor = "#EFF3FB";
                               
                            }
                            else 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                dataRow.style.backgroundColor = "#adadad";
                               
//                              var odataRow;
//                                  for (var m=1, n=sDataTable.tBodies[0].rows.length-1; m<n; m++) 
//                                  {
//                                     odataRow=sDataTable.tBodies[0].rows[m];
//                                     if(odataRow!=dataRow)
//                                     {
//                                        odataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//                                        odataRow.style.backgroundColor = "#EFF3FB";
//                                     }
//                                  }
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

  </script>
   </asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_Tech_MTO_STO.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Tech_MTO_STO"
    Title="变更调整" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript">
 /*检验输入数量并统计*/
        function checkRN(tb)
        {           
            var bgnum = parseFloat(tb.value);
            
            if (isNaN(bgnum))
            {
                alert("请输入正确的数量！");               
            }
            Statistic();
        }
   
    /*数据统计函数*/
        function Statistic() {
            var bgnum = 0;           
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
            {
               if(gv1.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked)  //dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked
               { 
                var val1 = gv1.rows[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value; //每行变更数量
                bgnum += parseFloat(val1);
               }
            }                    
         var lbsum = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[11].getElementsByTagName("span")[0];
         lbsum.innerHTML = bgnum.toFixed(4);
        }
        </script>  

    <div style="width: 100%;" align="center">
        <table width="100%">
            <tr>
                <td width="65%" align="left">                
                </td>
                <td width="35%">
                    <asp:Button ID="btn_stouse" runat="server" Text="确定" OnClick="btn_stouse_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div style="width:100%" align="left">   
         &nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
         <input id="continue" type="button" value="连选" onclick="consel()" />&nbsp;&nbsp;&nbsp;
         <input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        物料代码：<asp:Label ID="LabelMar" runat="server"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        变更数量：<asp:Label ID="LabelUseNum" runat="server"></asp:Label>
       
        <asp:GridView ID="GridView1" runat="server" EmptyDataText="无数据!" DataKeyNames="SQCODE" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound"
            AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="CheckBox1" /><%# Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PTC" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="MaterialCode" HeaderText="物料编码" HeaderStyle-Wrap="false"
                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MaterialName" HeaderText="物料名称" HeaderStyle-Wrap="false"
                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Standard" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Attribute" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GB" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Length" HeaderText="长" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Width" HeaderText="宽" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Unit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Number" HeaderText="库存数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" FooterStyle-Wrap="false" FooterStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="调整数量" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" FooterStyle-Wrap="false"  >
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxUserNum" runat="server" Width="60px" onclick="this.select();" onblur="checkRN(this)"
                            Text='<%#DataBinder.Eval(Container.DataItem, "stouse")%>'></asp:TextBox>  <%--onblur="checkRN(this)"--%>
                    </ItemTemplate>
                    <FooterTemplate >
                      <asp:Label runat="server" ID="LabelSumbg"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="false" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓库" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                        <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                            Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓位" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelLocation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Location")%>'></asp:Label>
                        <asp:Label ID="LabelLocationCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LocationCode")%>'
                            Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="定尺" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标识号" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" Height="20px" />
            <AlternatingRowStyle BackColor="White" />
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
      <br />
      <br />     
<div style="font-size:medium;text-align:center;width:100%" >出库单
    <asp:Panel runat="server" ID="Panelshow" Visible="false" Width="100%" >
      
     <asp:GridView runat="server" ID="GridViewshow" EmptyDataText="没有相关数据!"  EmptyDataRowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" >
       <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="OutCode" HeaderText="出库单号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PTC" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="MaterialCode" HeaderText="物料编码" HeaderStyle-Wrap="false"
                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="MaterialName" HeaderText="物料名称" HeaderStyle-Wrap="false"
                    ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Standard" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Attribute" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="GB" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Length" HeaderText="长" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Width" HeaderText="宽" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Unit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="RealNum" HeaderText="实发数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DEP" HeaderText="领料部门" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ZZBZ" HeaderText="制作班组" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />               
                    <asp:BoundField DataField="Sender" HeaderText="发料人" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OutWarehouse" HeaderText="发料仓库" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OutPosition" HeaderText="发料仓位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField DataField="Verifier" HeaderText="审核人" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="VeryfyTIME" HeaderText="审核时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center" />
                
            </Columns>
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" Height="20px" />
            <AlternatingRowStyle BackColor="White" />
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />   
    
    </asp:GridView>    
   </asp:Panel>
   
 <script type="text/javascript"> 
          
 function allsel()
  {
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
        tr[i].style.backgroundColor = "#adadad";
        Statistic();
       }
    }
  }

function cancelsel()
{
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
        tr[i].style.backgroundColor = "#EFF3FB";
        Statistic();
       }
    }
}

function consel()
{
    table=document.getElementById('<%= GridView1.ClientID %>');
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;

                for(var j=i+1;j<tr.length;j++)
                {
                    var nextobj=tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(nextobj!=null)
                    {
                    
                        if(nextobj.type.toLowerCase()=="checkbox" && nextobj.value!="")
                        {
                            if(nextobj.checked)
                            {
                                for(var k=i+1;k<j+1;k++)
                                {
                                    tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                    tr[k].style.backgroundColor = "#adadad";
                                    Statistic();
                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}     
      
        
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")  //点击某一行选中  
   function RowClick()
   {
          for (var i=1, j=sDataTable.tBodies[0].rows.length; i<j; i++) 
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
                               Statistic();
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
    
  </div>
</asp:Content>

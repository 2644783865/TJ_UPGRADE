<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.Master" AutoEventWireup="true" CodeBehind="SM_Warehouse_Allocation.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Allocation" Title="仓库调拨" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkRN(tb) {
            var num = parseFloat(tb.value);
            var wn = parseFloat(tb.parentNode.parentNode.getElementsByTagName("td")[13].getElementsByTagName("span")[0].innerHTML);
            if (isNaN(num)) {
                alert("请输入正确的数量！");
                tb.value = wn;
                Statistic();
            }
            if (num <= 0 || num > wn) {
                alert("调拨数量必须大于0且不大于可调拨数量！");
                tb.value = wn;
                Statistic();
            }
            Statistic();
        }

        function checkRQN(tb) {
            var rqn = Math.round(tb.value);
            var wqn = Math.round(tb.parentNode.parentNode.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
            if (isNaN(rqn)) {
                alert("请输入正确的数量！");
                tb.value = wqn;
                Statistic();
            }
            if (rqn < 0 || rqn > wqn) {
                alert("调拨数量必须大于0且不大于可调拨数量！");
                tb.value = wqn;
                Statistic();
            }
            Statistic();
        }

        function checkOut(tb) {
            var state = document.getElementById("<%=InputState.ClientID %>");
            if (state.value == "2") {
                tb.blur();
            }
        }
                
        function Statistic() {
            var tn = 0;
            var tqn = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                tn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
                tqn += parseFloat(val2);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[14].getElementsByTagName("span")[0];
            lbtn.innerHTML = tn.toFixed(4);
            var lbtqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[16].getElementsByTagName("span")[0];
            lbtqn.innerHTML = Math.round(tqn);
        }
        
        function ChoosePosition(tb) {
            var ws = document.getElementById('<%= DropDownListWarehouseIn.ClientID %>');
            var wscode = "";
            for (var i = 0; i < ws.options.length; i++) {
                if (ws.options[i].selected == true) {
                    wscode = ws.options[i].value;
                    break;
                }
            }
           var date=new Date();
           var time=date.getTime();
            var val = new Array(2);
            
            
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time, '', "dialogHeight: 550px; dialogWidth: 800px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) {
                tb.value = val[1];
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[20].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
//            else {
//                tb.value = "待查";
//                var par = tb.parentNode.parentNode;
//                var positioncode = par.getElementsByTagName("td")[20].getElementsByTagName("input")[1];
//                positioncode.value = "0";
//            }
        }
        

        function checkFinal() {
            var flag = true;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var ws1 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML;//仓库 
                var wl1 = gv1.rows[i].getElementsByTagName("td")[18].getElementsByTagName("span")[0].innerHTML;//仓位 
                var ws2 = gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("span")[0].innerHTML;
                var wl2 = gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value;
                if (ws2 == "--请选择--"||ws2 == "") {
                    gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("span")[0].style.color = "#FF0000";
                    alert("未选择调入仓库！");
                    flag = false;
                    return flag;
                     
                }
                if(wl2== "待查"||wl2 == ""){
                    gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].style.color = "#FF0000";
                    alert("未选择调入仓位！");
                    flag = false;
                    return flag;
                
                }
                if ((ws1 == ws2) && (wl1 == wl2)) {
                    gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#FF0000";
                    gv1.rows[i].getElementsByTagName("td")[18].getElementsByTagName("span")[0].style.color = "#FF0000";
                    gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("span")[0].style.color = "#FF0000";
                    gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].style.color = "#FF0000";
                    flag = false;
                }
                else {
                    gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("span")[0].style.color = "#0000FF";
                    gv1.rows[i].getElementsByTagName("td")[18].getElementsByTagName("span")[0].style.color = "#0000FF";
                    gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("span")[0].style.color = "#0000FF";
                    gv1.rows[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].style.color = "#0000FF";
                }
            }
            if (flag == false) {
                alert("调入仓库异常！");
            }
            return flag;
        }

        function append() {
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDAL", "","dialogWidth=1200px;dialogHeight=650px;status=no;help=no;scroll=no");
            return retVal;
        }

        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        }
        
         function tostorage() {
          var date=new Date();
          var time=date.getTime();
          window.open('SM_Warehouse_Query.aspx?FLAG=PUSHAL&id='+time,'_self');
       }
       function btnPrint_onclick() {
        window.showModalDialog('SM_Warehouse_Allocation_Print.aspx?alcode=<%=LabelCode.Text %>','',"dialogWidth=800px;dialogHeight=700px");   
        }
    
     function allsel()
      {
       var  table=document.getElementById('<%= GridView1.ClientID %>');
       var tr=table.getElementsByTagName("tr");
        for(var i=1;i<tr.length;i++)
        {
         if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
           {
            tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
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
            }
        }
    }

    function consel()
    {
        table=document.getElementById('<%= GridView1.ClientID %>');
        tr=table.getElementsByTagName("tr");
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
                                    }
                                }
                            }  
                        }
                    }
                }
            }
        }
    }
    
    </script>
         
 
        
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top">
    
    <cc1:TabPanel ID="Tab1" runat="server" HeaderText="调拨单" Height="90%" Width="100%">
    <ContentTemplate>   


        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <asp:Panel ID="Operation" runat="server">
    <table width="100%">
    <tr>
        <td>
        <input id="Close" type="button" value="关闭" onclick="closewin()" />
        <asp:Button ID="Append" runat="server" Text="追加"  OnClick="Append_Click" OnClientClick="return append()"/>
        <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click"/>
        <asp:Button ID="Save" runat="server" Text="保存"  OnClick="Save_Click"/>
        <asp:Button ID="Submit" runat="server" Text="提交" OnClick="Submit_Click"  />  
        <asp:Button ID="AntiSubmit" runat="server" Text="反提交" OnClick="AntiSubmit_Click"  /> 
        <asp:Button ID="AntiVerify" runat="server" Text="反审" OnClick="AntiVerify_Click"/>
        <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click" />
        <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click"/>
        <input id="btnPrint" runat="server" type="button" value="打印"  onclick="return btnPrint_onclick()" />
        <input id="ToStorage" type="button" value="到库存" onclick="tostorage()" />    

        </td>
        <td align="right">
           <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>       
        </td>
    </tr>
    </table>
    </asp:Panel>
    </div>
    </div>
    </div>
    
    <div class="box-wrapper" >
    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>            
                <asp:CheckBox ID="CheckBox1" runat="server" Text="计划跟踪号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="物料代码" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="物料名称" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="规格型号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="材质" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox6" runat="server" Text="国标" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox7" runat="server" Text="批号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox8" runat="server" Text="是否定尺" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox9" runat="server" Text="长" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox10" runat="server" Text="宽" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox11" runat="server" Text="可调拨数量" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                <asp:CheckBox ID="CheckBox12" runat="server" Text="可调拨张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" /> 
                <asp:CheckBox ID="CheckBox13" runat="server" Text="订单单号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />                      
                <asp:CheckBox ID="CheckBox14" runat="server" Text="标准件" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
          
        </tr> 
        <tr>
            <td>
             系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label></td>
            <td style=" font-size:x-large; text-align:center;" colspan="2">
                调拨单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/>
            </td>
            <td></td>
        </tr>
        <tr>
             <td >
                  <asp:Panel ID="Panel_Operation" runat="server" >
                       &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;
                          <input id="continue" type="button" value="连选" onclick="consel()" />&nbsp;&nbsp;&nbsp;
                          <input id="Button1" type="button" value="取消" onclick="cancelsel()" />
                        </asp:Panel>
                 </td>
        
            <td>
                &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
<%--                <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
--%>                <input type="text" id="InputState" style="display:none" runat="server"/>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
            </td>
            <td>
                调入仓库：<asp:DropDownList ID="DropDownListWarehouseIn" runat="server" AutoPostBack="true"
                 OnSelectedIndexChanged="DropDownListWarehouseIn_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
            调入仓位：<asp:DropDownList ID="DropDownListPositionIn" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                 OnSelectedIndexChanged="DropDownListPositionIn_SelectedIndexChanged">
                 <asp:ListItem Value="0">待查</asp:ListItem>
                </asp:DropDownList>
            </td>                    
        </tr>
    </table>
    </asp:Panel>
    

    <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto;position:static" Width="100%" Height="400px">
    
    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="100%" EmptyDataText="没有相关数据！" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Wrap="false">
                <ItemTemplate> 
                    <asp:Label ID="LineNumber" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                    <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>' Visible="false"></asp:Label>                                  
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
                <FooterTemplate>合计：</FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="物料代码">
                <ItemTemplate>
                    <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>' Visible="false"></asp:Label>              
                    <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            
            
             <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
                  
            <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>                
           
            <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>' ></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
           
            <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="可调拨数量" HeaderStyle-Wrap="false" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelWN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WN")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="数量" HeaderStyle-Wrap="false" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Number")%>' onblur="checkRN(this)" onfocus="checkOut(this)" Width="60px"></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalNumber" runat="server"></asp:Label>                
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="可调拨张(支)数" HeaderStyle-Wrap="false" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelWQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WQN")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="调拨张(支)数" HeaderStyle-Wrap="false" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "QN")%>' onblur="checkRQN(this)" onfocus="checkOut(this)" Width="85px"></asp:TextBox>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="LabelTotalQN" runat="server"></asp:Label>                
                </FooterTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>           
            
            <asp:TemplateField HeaderText="调出仓库" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelOutWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutWarehouse")%>'></asp:Label>
                    <asp:Label ID="LabelOutWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutWarehouseCode")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调出仓位" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelOutPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutPosition")%>'></asp:Label>
                    <asp:Label ID="LabelOutPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OutPositionCode")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调入仓库" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelInWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InWarehouse")%>' Width="80px"></asp:Label>
                    <asp:Label ID="LabelInWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InWarehouseCode")%>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调入仓位" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxInPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "InPosition")%>' Width="80px" onclick="ChoosePosition(this)"></asp:TextBox>
                    <input type="text" id="InputInPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "InPositionCode")%>' style="display:none" runat="server"/>
                 </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="计划模式" Visible="false" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>' Width="100px"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle Wrap="False" />
            </asp:TemplateField>            
        </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
    <RowStyle BackColor="#EFF3FB"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
     <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    
    </asp:Panel>
    
    <asp:Panel ID="FooterPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td align="left">
              &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:DropDownList ID="DropDownListDep" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;制 &nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server" Enabled="false"></asp:Label>
                <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
            </td>
       </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
    </table>
    </asp:Panel>

    </div>
    
    
    </ContentTemplate>
        </asp:UpdatePanel>

    
    
    </ContentTemplate>
    </cc1:TabPanel>
    
    <cc1:TabPanel ID="Tab2" runat="server" HeaderText="调拨单审核" Height="90%" Width="100%">
    <ContentTemplate>
    
    <div>
    <div><p style="color:Black;text-align:center;font-size:x-large;">调拨单审核<asp:Image ID="ImageVerify2" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false"/></p></div>
    <div>
    <table style="border:solid 1px black;">
        <tr style="border:solid 1px black;width:100%;">
            <td style="border-right:solid 1px black;width:30%;" rowspan="2">
                <p style="text-align:center;">一级审核</p>
            </td>
            <td>
                <asp:TextBox ID="TextBoxAdvice" runat="server" BorderStyle="None" Height="200px" Width="1000px" style="overflow:auto;" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                审核人：<asp:Label ID="LabelVerifier" runat="server" BorderStyle="None" Width="100px"></asp:Label>
                <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                日期：<asp:Label ID="LabelApproveDate" runat="server" BorderStyle="None" Width="100px"></asp:Label>&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="return checkFinal()" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Deny" runat="server" Text="驳回" OnClick="Deny_Click" />&nbsp;&nbsp;&nbsp;
                <input id="Closewin" type="button" value="关闭" onclick="closewin()" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </ContentTemplate>
    </cc1:TabPanel>
    
    </cc1:TabContainer>
    <script type="text/javascript">//点击某行选中
  
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                
                var dataRow = sDataTable.tBodies[0].rows[i];
                var clicked = false;
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
                                dataRow.style.backgroundColor = "#EFF3FB";
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
                                dataRow.style.backgroundColor = "#D1DDF1";
                                clicked = true;
//                                 var odataRow;
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

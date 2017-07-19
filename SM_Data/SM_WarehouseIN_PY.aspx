<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true" CodeBehind="SM_WarehouseIN_PY.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_PY" Title="盘盈入库单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>   

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    

    <script src="SM_JS/KeyControl.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        
        
        /*关闭页面时，提示保存信息*/
//         function window.onbeforeunload()
//        {
//             event.returnValue ="您确定要离开当前页面？";
//        }
      
        /*检查输入的长度并根据长度该表批号*/
        function checklength(tb)
        {
            var length = parseFloat(tb.value);
            if (isNaN(length) || length < 0)
            {
                alert("请正确输入长度！");
                tb.value = 0;
            }
        }

        /*检查输入的宽度并根据长度该表批号*/        
        function checkwidth(tb)
        {
            var width = parseFloat(tb.value);
            if (isNaN(width) || width < 0)
            {
                alert("请正确输入宽度！");
                tb.value = 0;
            }
        }
        
        /*检查输入的张数或支数并调整批号*/
        function checkQuantity(tb) {
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            var quantity = parseFloat(tb.value);
            if (isNaN(quantity)) {
                alert("请正确输入张数或支数！");
                tb.value = 0;
            }
            if ((color == 0) && (quantity < 0)) {
                tb.value = -tb.value;
            }
            if ((color == 1) && (quantity > 0)) {
                tb.value = -tb.value;
            }
            Statistic();
        }

        /*根据填写的批号生成标准批号*/
        function checkLotNum(tb) {
            if (tb.value == "") {return;}            
            var lotnum = parseFloat(tb.value);
            if (isNaN(lotnum)||lotnum<=0||lotnum>999) {
                alert("请正确输入批号！");
                tb.value = "";
                return;
            }
            var par = tb.parentNode.parentNode;
            var lotnum = par.getElementsByTagName("td")[11].getElementsByTagName("input")[0];
            var code = document.getElementById('<%= LabelCode.ClientID %>').innerHTML;
            if (lotnum.value != "") {
                var stringlist = lotnum.value.split('-');
                if ((stringlist[0].length == 1)) { stringlist[0] = "00" + stringlist[0]; }
                else if (stringlist[0].length == 2) { stringlist[0] = "0" + stringlist[0]; }
                else { }
                lotnum.value = stringlist[0] + "-" + code;
            }       
        }

        /*检验输入数量并统计*/
        function checkRN(tb)
        {
            var flag = true;            
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            var realnum = parseFloat(tb.value);
            
            if (isNaN(realnum))
            {
                alert("请输入正确的数量！");
                tb.value = 0;
                realnum = tb.value;
                var par = tb.parentNode.parentNode;
                var unitprice = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
                var ctup = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML);

                var amount = par.getElementsByTagName("td")[18].getElementsByTagName("input")[0];
                amount.value = (realnum * unitprice).toFixed(2);
                var cta = par.getElementsByTagName("td")[19].getElementsByTagName("input")[0];
                cta.value = (realnum * ctup).toFixed(2);
                Statistic();
            }
            
            if ((color == "0") && (realnum < 0))
            {
                tb.value = -tb.value;
                realnum = tb.value;
                var par = tb.parentNode.parentNode;
                var unitprice = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
                var ctup = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML);

                var amount = par.getElementsByTagName("td")[18].getElementsByTagName("input")[0];
                amount.value = (realnum * unitprice).toFixed(2);
                var cta = par.getElementsByTagName("td")[19].getElementsByTagName("input")[0];
                cta.value = (realnum * ctup).toFixed(2);
                Statistic();
            }
            if ((color == "1")&&(realnum>0))
            {
                tb.value = -tb.value;
                realnum = tb.value;
                var par = tb.parentNode.parentNode;
                var unitprice = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
                var ctup = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML);

                var amount = par.getElementsByTagName("td")[18].getElementsByTagName("input")[0];
                amount.value = (realnum * unitprice).toFixed(2);
                var cta = par.getElementsByTagName("td")[19].getElementsByTagName("input")[0];
                cta.value = (realnum * ctup).toFixed(2);
                Statistic();
            }
            var par = tb.parentNode.parentNode;
            var unitprice = parseFloat(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
            var ctup = parseFloat(par.getElementsByTagName("td")[17].getElementsByTagName("span")[0].innerHTML);

            var amount = par.getElementsByTagName("td")[18].getElementsByTagName("input")[0];
            amount.value = (realnum * unitprice).toFixed(2);
            var cta = par.getElementsByTagName("td")[19].getElementsByTagName("input")[0];
            cta.value = (realnum * ctup).toFixed(2);
            Statistic();
        }

        /*数据统计函数*/
        function Statistic() {
            var tn = 0;
            var tq = 0;
            var ta = 0;
            var tcta = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
            {
                var val1 = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
                tn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                tq += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value;
                ta += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value;
                tcta += parseFloat(val4);
            }
            var lbtn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[13].getElementsByTagName("span")[0];
            lbtn.innerHTML = tn.toFixed(4);
            var lbtq = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[14].getElementsByTagName("span")[0];
            lbtq.innerHTML = tq.toFixed(4);
            var lbta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[18].getElementsByTagName("span")[0];
            lbta.innerHTML = ta.toFixed(2);
            var lbtcta = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[19].getElementsByTagName("span")[0];
            lbtcta.innerHTML = tcta.toFixed(2);
        }

        /*填充仓位*/
        function ChoosePosition(tb)
         {
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (color == "1")
            {
                return;
            }
            var ws = document.getElementById('<%= DropDownListWarehouse.ClientID %>');
            var wscode = "";
            for (var i = 0; i < ws.options.length; i++)
            {
                    if (ws.options[i].selected == true)
                    {
                        wscode = ws.options[i].value;
                        break;
                    }
            }

           var date=new Date();
           var time=date.getTime();

            var val = new Array(2);
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time, '', "dialogHeight: 500px; dialogWidth: 600px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[22].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
            else
            {
                tb.value = "待查";
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[22].getElementsByTagName("input")[1];
                positioncode.value = "0";
            }
        }

        /*提交时页面检验*/
        function checkPage() 
        {
            var flag = true;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++)
             {
                 var num = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
//                 var quantity = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value;
                 var warehouse = gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("span")[0].innerHTML;
                 
                 var position=gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                 
                if (num == 0) {
                    gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].style.color = "#FF0000";
                    alert("请填写正确的入库数量！");
                    return false;
                }
                else {
//                    gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].style.color = "#0000FF";
                }
//                if (quantity == 0) {
//                    gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].style.color = "#FF0000";
//                    flag = false;
//                }
//                else {
//                    gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].style.color = "#0000FF";
//                }
                if (warehouse == "--请选择--") 
                {
                    gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("span")[0].style.color = "#FF0000";
                    alert("仓库未填写！");
                    return false;
                }
                else
                {
                    gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("span")[0].style.color = "#0000FF";
                }
                if (position == "待查"||position == "") 
                {
                    gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].style.color = "#FF0000";
                    alert("仓位未填写！");
                    return false;
                }
                else
                {
                    gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].style.color = "#0000FF";
                }
            }
            if (flag == false)
            {
                alert("数据错误请检查！");
            }
            
            is_save=true;
            
            return flag;
        }

        /*追加条目*/
        function append() 
        {
            var suppliercode = document.getElementById('<%= InputSupplierCode.ClientID %>').value;
            var date=new Date();
            var time=date.getTime();
            var retVal = window.showModalDialog("SM_WarehouseIN_WGPush.aspx?FLAG=APPEND&&SUPPLIERCODE=" + suppliercode+"&&id="+time, "", "dialogWidth=1200px;dialogHeight=650px;status=no;help=no;scroll=no");
            return retVal;
        }

        /*关闭窗口*/
        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close(); 
        } 
        
        
        function btnPrint_onclick() {
        var date=new Date();
        var time=date.getTime();
        window.showModalDialog("SM_WarehouseIn_Print.aspx?incode=<%=LabelCode.Text %>&&id="+time,'',"dialogWidth=960px;dialogHeight=580px;status=no;help=no;");   
        }
        
        function btnorder_onclick() {
        window.open("SM_WarehouseIN_WGPush.aspx?FLAG=PUSH");   
        }
           
        function delconfirm() {
        var retVal = confirm("确定要删除所选定入库单？");
        return retVal;
    }
    
    function allsel()
  {
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
//        tr[i].style.backgroundColor ='#55DF55'; 
        }
    }
  }

function cancelsel()
{
   var  table=document.getElementById("ctl00_PrimaryContent_GridView1");
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
       if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=false;
//        tr[i].style.backgroundColor='#EFF3FB'; 
        }
    }
}

function consel()
{
    table=document.getElementById("ctl00_PrimaryContent_GridView1");
    tr=table.getElementsByTagName("tr");//这里的tr是一个数组
    for(var i=1;i<(tr.length-1);i++)
    {
        obj=tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
        if(obj.type.toLowerCase()=="checkbox" && obj.value!="")
        {
            if(obj.checked)
            {
                obj.checked=true;
//                obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
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
//                                    tr[k].style.backgroundColor ='#55DF55'; 
                                }
                            }
                        }  
                    }
                }
            }
        }
    }
}
 function checkme(obj) 
     { 
        if(obj.checked) 
        { 
           obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
//           oldcolor='LawnGreen'; 
        } 
        else 
        { 
           obj.parentNode.parentNode.style.backgroundColor='#EFF3FB'; 
//           oldcolor=''; 
        } 
      }   
     
    
    
    </script>
    
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> --%>
    <div class="box-inner" >
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td align="left">
            
            <input id="Close" type="button" value="关闭" onclick="closewin()" />&nbsp;|
           
            <asp:Button ID="Save" runat="server" Text="保存" CausesValidation="False" OnClick="Save_Click" />
                &nbsp;|
            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="return checkPage()"/>
                &nbsp;|
             <asp:Button ID="FS" runat="server" Text="反审" OnClick="FS_Click" OnClientClick="return confirm('确定要反审该入库单？');"/>
                &nbsp;|
            
             <asp:Button ID="DelForm" runat="server" Text="删单" OnClick="DelForm_Click" OnClientClick="return delconfirm()"/>
                &nbsp;|
             
             <asp:Button ID="GL" runat="server" Text="关联单据" OnClick="GL_Click"/>&nbsp;|
             
             <input id="btnPrint" runat="server" type="button" value="打印" 
                  onclick="return btnPrint_onclick()" />&nbsp;|
                  
            </td>
            
            <td align="left" >
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red" ></asp:Label>
            </td>    
        </tr>
    </table>
    </div>
    </div>
    </div>
    
  
   
    <div class="box-wrapper" >
    <div class="box-outer">
   
    <asp:Panel ID="HeadPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
            <td>
            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
            </td>
            <td style=" font-size:large; text-align:center;" colspan="3">
                盘盈入库单<asp:Image ID="ImageVerify" runat="server" 
                    ImageUrl="~/Assets/images/shenhe.gif" Visible="False"/>
            </td>
            <td align="right">
                <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false"/>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" style="width:20%;">
                &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server"></asp:Label>
                <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="LabelGJState" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="LabelHXState" runat="server" Visible="False"></asp:Label>
                <input type="text" id="InputColour" style="display:none" runat="server"/>
        </td>
            <td align="left" style="width:20%;">
                &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" Enabled="True" 
                    Format="yyyy-MM-dd" TargetControlID="TextBoxDate">
                </cc1:CalendarExtender>
            </td>
            <td align="left" style="width:22%;">
                供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                <input type="text" id="InputSupplierCode" style="display:none" runat="server"/>
            </td> 
            <td align="center" style="width:18%;">
                收料仓库 ：<asp:DropDownList ID="DropDownListWarehouse" runat="server" 
                    AutoPostBack="True"  OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                  
                </asp:DropDownList>
            </td>
            <td align="left" style="width:20%;">
               收料仓位 ：
               
                <asp:DropDownList ID="DropDownListPosition" runat="server" AutoPostBack="True" AppendDataBoundItems="true"  OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                 <asp:ListItem Value="0">待查</asp:ListItem>
                </asp:DropDownList>
               
            </td>
        </tr>
         <tr>
            <td align="left" style="width:20%;">
              货单编号：<asp:TextBox ID="TextBoxHDBH" runat="server"></asp:TextBox>
            </td>
            <td align="left" style="width:20%;">
               &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxAbstract" runat="server" ></asp:TextBox>
            </td>
            <td>
            <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <div>
           &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input id="continue" type="button" value="连选" onclick="consel()"/>
           &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()"/>
    </div>
    <asp:Panel ID="PanelBody" runat="server"  style="overflow:auto; position:relative; margin:2px" Width="100%" Height="400px">
    <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" 
     ShowFooter="True" EmptyDataText="没有相关数据！"    AllowSorting="true"
            OnRowDataBound="GridView1_RowDataBound" onsorting="GridView1_Sorting">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server"  />
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Labellinenumber" runat="server" Text='<%#Container.DataItemIndex+1%>' Width="15px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField SortExpression="PTC" HeaderText="计划跟踪号">
                    <ItemTemplate>
                        <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>' Width="130px"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelFoot" runat="server" Text="合计:" ></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField SortExpression="MaterialCode" HeaderText="物料编码">
                    <ItemTemplate>
                         <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>' Width="80px"></asp:Label>                  
                         <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField SortExpression="MaterialName" HeaderText="物料名称">
                    <ItemTemplate>
                        <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>' Width="80px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField SortExpression="MaterialStandard" HeaderText="型号规格">
                    <ItemTemplate>
                        <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>' Width="80px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                 <asp:TemplateField SortExpression="Attribute"  HeaderText="材质">
                    <ItemTemplate>
                        <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>' Width="80px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                
                  <asp:TemplateField HeaderText="国标">
                    <ItemTemplate>
                        <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>' Width="110px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                
                <asp:TemplateField SortExpression="Fixed" HeaderText="是否定尺">
                    <ItemTemplate>
                        <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>' Width="60px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>                
                <asp:TemplateField SortExpression="Length" HeaderText="长(mm)" >
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>' Width="60px" onkeyup="grControlFocus(this)" onclick="getSelect(this)"  onblur="checklength(this)"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="宽(mm)">        
                    <ItemTemplate >
                        <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>' Width="60px" onkeyup="grControlFocus(this)"  onblur="checkwidth(this)"></asp:TextBox>
                         <%--<asp:RegularExpressionValidator ID="widthRegularExpressionValidator" runat="server" ValidationExpression="^[1-9]\d*(\.)?\d*|0\.\d*|0$" ErrorMessage="" Text="*" ControlToValidate="TextBoxWidth" ></asp:RegularExpressionValidator>--%>
                       </ItemTemplate>
                       <ItemStyle Wrap="False" />
                </asp:TemplateField>
              
               
                <asp:TemplateField HeaderText="批号">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>' Width="120px" onkeyup="grControlFocus(this)" onblur="checkLotNum(this)"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位">
                    <ItemTemplate>
                        <asp:Label ID="LabelUnit" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>' Width="40px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="实收数量">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>' Width="60px" onkeyup="grControlFocus(this)" onblur="checkRN(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="张（支）数">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' Width="60px" onkeyup="grControlFocus(this)" onblur="checkQuantity(this)"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单价">
                    <ItemTemplate>
                        <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>' Width="60px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="税率">
                    <ItemTemplate>
                      <asp:Label ID="LabelTaxRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TaxRate")%>' Width="20px"  ></asp:Label>%
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税单价">
                    <ItemTemplate>
                        <asp:Label ID="LabelCTUP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CTUP")%>' Width="60px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="金额">
                    <ItemTemplate>
                         <input type="text" id="InputAmount" value='<%#DataBinder.Eval(Container.DataItem, "Amount")%>' readonly="readonly" runat="server" style="width: 60px" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalAmount" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含税金额">
                    <ItemTemplate>
                        <input type="text" id="InputCTA" value='<%#DataBinder.Eval(Container.DataItem, "CTA")%>' readonly="readonly" runat="server" style="width: 60px" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LabelTotalCTA" runat="server"></asp:Label>
                    </FooterTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>' Width="100px"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓库">
                    <ItemTemplate>
                        <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'  Width="60px"></asp:Label>
                        <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓位">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>' Width="60px" onclick="ChoosePosition(this)"></asp:TextBox>
                         <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>' style="display:none" runat="server"/>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="订单单号">
                    <ItemTemplate>
                        <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>' Width="120px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="采购方式" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="LabelCGMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CGMODE")%>' Width="60px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划模式">
                    <ItemTemplate>
                        <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>' Width="60px"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Wrap="False" /> 
                </asp:TemplateField>
            </Columns>
    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
    <RowStyle BackColor="#EFF3FB"/>
    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False"/>
     <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />     
    </yyc:SmartGridView>
    </asp:Panel>
     
     <asp:Panel ID="FooterPanel" runat="server" Width="100%">
    <table width="100%">
        <tr>
          
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                <asp:Label ID="LabelDocCode" runat="server" Visible="False"></asp:Label>
            </td>
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;收料员：<asp:DropDownList ID="DropDownListAcceptance" runat="server">
                </asp:DropDownList>
            </td>
            <td align="left" style="width:25%;">
                &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server"></asp:Label>
                <asp:Label ID="LabelVerifierCode" runat="server" Visible="False"></asp:Label>
            </td>
            <td align="left" style="width:25%;">
                审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
            </td>
          
        </tr>
      
    </table>
    </asp:Panel>
    
    </div>
    </div>

<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
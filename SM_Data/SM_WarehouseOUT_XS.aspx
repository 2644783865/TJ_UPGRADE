<%@ Page Title="销售出库" Language="C#" MasterPageFile="~/Masters/BaseMaster.master"
    AutoEventWireup="true" CodeBehind="SM_WarehouseOUT_XS.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_XS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript" language="javascript">
        function checkLength(tb) {
            var length = parseFloat(tb.value);
            if (isNaN(length) || length < 0) {
                alert("请输入正确的长度！");
                tb.value = 0;
            }
        }

        function checkWidth(tb) {
            var width = parseFloat(tb.value);
            if (isNaN(width) || width < 0) {
                alert("请输入正确的长度！");
                tb.value = 0;
            }
        }
        
        function checkRN(tb) {
            var rn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dn = parseFloat(par.getElementsByTagName("td")[13].getElementsByTagName("span")[0].innerHTML);
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rn)) {
                alert("请输入正确的数量！");
                tb.value = dn;
                Statistic();
                
                js(tb);
            }
            if ((color == '0') && (rn > dn)) {
                alert("出库数量不能大于库存数量！");
                tb.value = dn;
                Statistic();
                
                js(tb);
            }
            if (rn == 0) {
                tb.value = dn;
                Statistic();
                
                js(tb);
            }
            if ((color == '0') && (rn < 0)) {
                tb.value = -rn;
                if (tb.value > dn) {
                    tb.value = dn;
                }
                Statistic();
                js(tb);
            }
            if ((color == '1') && (rn > 0)) {
                tb.value = -rn;
                Statistic();
                js(tb);
            }
            
            if((color == '1') && (parseFloat(tb.value) <dn)){
            
               alert("红单数量不能多于原单数量！");
               
               tb.value = dn;
               
               Statistic();
               
               js(tb);
            }
            
             
             
             js(tb);
             
             Statistic();            
             
        }
//辅助单位（张（支））
        function checkRQN(tb) {
            var rqn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dqn = Math.round(par.getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML);
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            if (isNaN(rqn)) {
                alert("请输入正确的数量！");
                tb.value = dqn;
                Statistic();
            }
            if ((color == '0') && (rqn > dqn)) {
                alert("出库数量不能大于库存数量！");                
                tb.value =dqn;
                Statistic();
            }            
            if ((color == '0') && (rqn < 0)) {
                tb.value = -rqn;
                if (tb.value > dqn) {
                    tb.value = dqn;
                }
                Statistic();
            }
            if ((color == '1') && (rqn > 0)) {
                tb.value = -rqn;
                Statistic();
            }
             if ((color == '1') && (parseFloat(tb.value)<dqn)) {
                alert("红单数量不能大于原单数量！");   
                tb.value = dqn;
                Statistic();
            }
            Statistic();
        }
          function checkDJ(tb) {
                var dj = parseFloat(tb.value);
                var par = tb.parentNode.parentNode;
                var num=par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                if(isNaN(dj))
                {
                alert("请输入正确的单价！");
                tb.value = 0;     
                }
                if(dj<0)
                {
                tb.value =-dj;  
                }
                var je=Math.round(num*parseFloat(tb.value)*100)/100;
                par.getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=je;
                Statistic();
           }
           function checkJE(tb) {
                var je = parseFloat(tb.value);
                var par = tb.parentNode.parentNode;
                var num=par.getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                if(isNaN(je))
                {
                alert("请输入正确的金额！");
                tb.value = 0;     
                }
                if(je<0)
                {
                tb.value =-je;  
                }
                var dj=Math.round((parseFloat(tb.value)/num)*10000)/10000;
                par.getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=dj;
                Statistic();
           }
        
        function js(tb)
        {
                var num=parseFloat(tb.value);
               
                var par = tb.parentNode.parentNode;
                
                var dj=parseFloat(par.getElementsByTagName("td")[22].getElementsByTagName("input")[0].value);
                if(isNaN(dj))
                dj=0;
                var je=num*dj;
//                if(je=0)
//                {par.getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=""}
//                else
//                {
                par.getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=je.toFixed(4);
        }
        
        function Statistic() {
            var tdn = 0;
            var trn = 0;
            var tdqn = 0;
            var trqn = 0;
            var tje = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("span")[0].innerHTML;
                tdn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                trn += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("span")[0].innerHTML;
                tdqn += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
                trqn += parseFloat(val4);
                var val5=gv1.rows[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;                
                tje+=parseFloat(val5);
            }
            var lbtdn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[13].getElementsByTagName("span")[0];
            lbtdn.innerHTML = tdn.toFixed(4);
            var lbtrn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[14].getElementsByTagName("span")[0];
            lbtrn.innerHTML = trn.toFixed(4);
            var lbtdqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[15].getElementsByTagName("span")[0];
            lbtdqn.innerHTML = tdqn.toFixed(4);
            var lbtrqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[16].getElementsByTagName("span")[0];
            lbtrqn.innerHTML = trqn.toFixed(4);
            var lbje = gv1.rows[gv1.rows.length-1].getElementsByTagName("td")[23].getElementsByTagName("span")[0];
            lbje.innerHTML=tje.toFixed(4);
        }

        
        function checkPage() {
//              debugger
            var flag = true;
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
            if (color == 0) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                    var val2 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
                    var val3=gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;//单价
                    var val4=gv1.rows[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value;//金额
                    if (val1 <= 0) {
                        gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 <0) {
                        gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if(val3=="")
                     {    
                            flag = false;
                            alert("销售出库单价不能为空！");
                            return flag;               
                    }
                    else
                    {
                        if(val3<=0||isNaN(val3))
                        {
                            gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].style.color = "#FF0000";
                            flag = false;
                            alert("销售出库单价必须大于0！");
                            return flag;
                        }
                    }
                }
                if (flag == false) {
                    alert("蓝字出库单数量必须大于0！");
                } 
            }
            if (color == 1) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value;
                    var val2 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value;
                    if (val1 >= 0) {
                        gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 >0) {
                        gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                }
                if (flag == false) {
                    alert("红字出库单数量必须小于0！");
                } 
            }
            
         
            
            return flag;
        }

        function append() {
        var date=new Date();
        var time=date.getTime();
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDOUTXS&&id="+time, "", "dialogWidth=1200px;dialogHeight=650px;status=no;help=no;scroll=no");
            return retVal;
        }

        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        } 
        
//        function btnPrint_onclick() {
//        
//        var date=new Date();
//        var time=date.getTime();
//        window.showModalDialog('SM_WarehouseOUT_Print.aspx?outcode=<%=LabelCode.Text %>&&id='+time,'',"dialogWidth=800px;dialogHeight=700px");   
//        }

       function SumPrint() {
        var date=new Date();
        var time=date.getTime();
        window.showModalDialog("SM_WarehouseOUT_Print.aspx?outcode=<%=LabelCode.Text %>&&oper=sum&&id="+time,'',"dialogWidth=1200px;dialogHeight=580px;status=no;help=no;");   
        }
      function DetailPrint() {
        var date=new Date();
        var time=date.getTime();
        window.showModalDialog("SM_WarehouseOUT_Print_Detail.aspx?outcode=<%=LabelCode.Text %>&&oper=detail&&id="+time,'',"dialogWidth=1200px;dialogHeight=580px;status=no;help=no;");   
        }

      function viewCondition(){
   document.getElementById("<%=PanelSplitLineHandle.ClientID%>").style.display='block';
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


    </script>

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <input id="Close" type="button" value="关闭" onclick="closewin()" />
                            <asp:Button ID="Append" runat="server" Text="追加" OnClick="Append_Click" OnClientClick="return append()" />
                            <asp:Button ID="Split" runat="server" Text="拆分" OnClientClick="viewCondition()" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSplit" runat="server" TargetControlID="Split"
                                PopupControlID="PanelSplitLine" Drag="True" PopupDragHandleControlID="PanelSplitLineHandle"
                                Enabled="True" X="500" Y="300" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />
                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" />
                            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="return checkPage()" />
                            <asp:Button ID="AntiVerify" runat="server" Text="反审核" OnClick="AntiVerify_Click" />
                            <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click" />
                            <asp:Button ID="PushRed" runat="server" Text="下推红字" OnClick="PushRed_Click" />
                            <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click" />
                            <%--<input id="btnPrint" runat="server" type="button" value="打印" onclick="return btnPrint_onclick()" />--%>
                            <asp:Button ID="SumPrint" runat="server" Text="汇总打印" OnClientClick="SumPrint()" />
                            <asp:Button ID="Print" runat="server" Text="明细打印" OnClientClick="DetailPrint()"/>
                            <asp:Button ID="tostorge" runat="server" Text="到库存" OnClick="tostorge_Click" CausesValidation="false" />
                        </td>
                        <td>
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelSplitLineHandle" runat="server" Style="display: none">
        <asp:Panel ID="PanelSplitLine" runat="server">
            <table style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td align="center" colspan="2">
                        条目拆分选项
                    </td>
                </tr>
                <tr>
                    <td>
                        记录拆分模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListSplitMode" runat="server" RepeatColumns="2"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="均分" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="复制" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        记录复制模式：
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListSplitMode2" runat="server" RepeatColumns="2"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="插入" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="追加" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        记录拆分行数：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxSplitLineNum" runat="server" Text="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonSplitOK" runat="server" OnClick="ButtonSplitOK_Click" Text="确定" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonSplitCancel" runat="server" OnClick="ButtonSplitCancel_Click"
                            Text="取消" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:Panel ID="HeadPanel" runat="server" Style="position: relative; margin: 2px"
                Width="98%">
                <table style="width: 100%; border: solid 1px black;">
            <tr>
                <td>
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="计划跟踪号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="物料代码" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="物料名称" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="规格型号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox5" runat="server" Text="材质" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox6" runat="server" Text="国标" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox7" runat="server" Text="批号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox8" runat="server" Text="是否定尺" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                   <%-- <asp:CheckBox ID="CheckBox9" runat="server" Text="库存长(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />--%>
                    <asp:CheckBox ID="CheckBox10" runat="server" Text="长" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox11" runat="server" Text="宽" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox12" runat="server" Text="即时库存张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox13" runat="server" Text="实发张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    <asp:CheckBox ID="CheckBox14" runat="server" Text="标准件" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                </td>
            </tr>
        </table>
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            销售出库单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                            <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                runat="server" style="display: none"></asp:Label>
                                  <asp:Label ID="LabelTrueCode" runat="server" Text='<%#Eval("TrueCode")%>'  ></asp:Label>
                                
                                
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;期：<asp:TextBox
                                ID="TextBoxDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            收款时间：<asp:TextBox ID="TextBoxCollectionDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderTextBoxCollectionDate" runat="server" TargetControlID="TextBoxCollectionDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                            <asp:Panel ID="Panel_print" runat="server" Visible="false">
                                打印次数：<asp:TextBox ID="TextBoxPrintTime" runat="server"></asp:TextBox></asp:Panel>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;摘要&nbsp;&nbsp;&nbsp;：<asp:TextBox ID="TextBoxAbstract" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;购货单位：<asp:DropDownList ID="DropDownListCompany" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;销售业务类型：<asp:DropDownList ID="DropDownListProcessType" runat="server">
                                <asp:ListItem Value="0">销售出库类型</asp:ListItem>
                                <asp:ListItem Value="1">受托出库类型</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            销售方式：<asp:DropDownList ID="DropDownListSellWay" runat="server">
                                <asp:ListItem Value="0">现销</asp:ListItem>
                                <asp:ListItem Value="1">赊销</asp:ListItem>
                                <asp:ListItem Value="2">零售</asp:ListItem>
                                <asp:ListItem Value="3">受托带销</asp:ListItem>
                                <asp:ListItem Value="4">委托代销</asp:ListItem>
                                <asp:ListItem Value="5">分期收款销售</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;
                            <asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody" runat="server" Style="overflow: auto; margin: 2px" Width="98%"
                Height="400px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true" AllowSorting="true"  OnSorting="GridView1_Sorting"
                    EmptyDataText="没有相应的记录！" EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UniqueID")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划跟踪号" HeaderStyle-Wrap="false" SortExpression="PTC">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                合计:
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料代码" HeaderStyle-Wrap="false" SortExpression="MaterialCode">
                            <ItemTemplate>
                                <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物料名称" HeaderStyle-Wrap="false" SortExpression="MaterialName">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格型号" HeaderStyle-Wrap="false" SortExpression="MaterialStandard">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="材质" HeaderStyle-Wrap="false" SortExpression="Attribute">
                            <ItemTemplate>
                                <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="国标" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="批号" HeaderStyle-Wrap="false" SortExpression="LotNumber">
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
                        <asp:TemplateField HeaderText="长" HeaderStyle-Wrap="false" SortExpression="Length">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'
                                    onblur="checkLength(this)" Width="50px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="宽" HeaderStyle-Wrap="false" SortExpression="Width">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'
                                    onblur="checkWidth(this)" Width="50px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="即时库存数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelDN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalDN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                            <FooterStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                    onblur="checkRN(this)" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="即时库存张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelDQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DQN")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalDQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                            <FooterStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'
                                    onblur="checkRQN(this)" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false" SortExpression="OrderID">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                    Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发料仓库" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'></asp:Label>
                                <asp:Label ID="LabelPositionCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售单价" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxDJ" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitCost")%>' Width="80px" onblur="checkDJ(this)"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="销售金额" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxJE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Cost")%>' Width="80px" onblur="checkJE(this)"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalXSJE" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <RowStyle BackColor="#EFF3FB" Wrap="false" />
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" Wrap="false" />
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="FooterPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 20%;">
                            &nbsp;&nbsp;发&nbsp;&nbsp;&nbsp;料：<asp:DropDownList ID="DropDownListSender" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 20%;">
                            业务员：<asp:DropDownList ID="DropDownListClerk" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 20%;">
                            制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%;">
                            审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                            <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 20%;">
                            审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>            
            
          <script type="text/javascript">  
              var sDataTable=document.getElementById("<%=GridView1.ClientID %>")  
          
               function RowClick()//点击某一行选中
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
        </div>
    </div>
</asp:Content>

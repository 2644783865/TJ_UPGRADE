<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseIN_WG.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseIN_WG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </cc1:ToolkitScriptManager>

    <script src="SM_JS/KeyControl.js" type="text/javascript"></script>
    
        <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 100px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        
        
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
        
        /*检查输入的张数或支数*/
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

///*原来批号生成规则填写的批号生成标准批号*/
//        function checkLotNum(tb) {
//            if (tb.value == "") {return;}            
//            var lotnum = parseFloat(tb.value);
//            if (isNaN(lotnum)||lotnum<=0||lotnum>999) {
//                alert("请正确输入批号！");
//                tb.value = "";
//                return;
//            }
//            var par = tb.parentNode.parentNode;
//            var lotnum = par.getElementsByTagName("td")[11].getElementsByTagName("input")[0];
//            var code = document.getElementById('<%= LabelCode.ClientID %>').innerHTML;
//            if (lotnum.value != "") {
//                var stringlist = lotnum.value.split('-');
//                if ((stringlist[0].length == 1)) { stringlist[0] = "00" + stringlist[0]; }
//                else if (stringlist[0].length == 2) { stringlist[0] = "0" + stringlist[0]; }
//                else { }
//                lotnum.value = stringlist[0] + "-" + code;
//            }       
//        }

        /*根据填写的批号生成标准批号*/
        function checkLotNum(tb) {
            if (tb.value == "") {return;}            
            var lotnum = parseFloat(tb.value);//转为数字
            if (isNaN(lotnum)||lotnum<=0||lotnum>999) {
                alert("请正确输入批号！");
                tb.value = "";
                return;
            }
            var par = tb.parentNode.parentNode;
            var lotnum = par.getElementsByTagName("td")[11].getElementsByTagName("input")[0];
            var code = document.getElementById('<%= LabelCode.ClientID %>').innerText;
            var secondpart =parseInt(code.substring(1,10),10);
            var poder = par.getElementsByTagName("td")[23].getElementsByTagName("span")[0].innerHTML;            
            var thirdpart =parseInt(poder.substring(poder.length-8,poder.length),10);
            if (lotnum.value != "") {
                var stringlist = lotnum.value.split('-');
                if ((stringlist[0].length == 1)) { stringlist[0] = "00" + stringlist[0]; }
                else if (stringlist[0].length == 2) { stringlist[0] = "0" + stringlist[0]; }
                else { }
                lotnum.value = stringlist[0] + "-" + secondpart + "-" + thirdpart;
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

  
        
        function changeStaff(tb)
        {
          var staff=tb.value
          tb.value=staff.split('-')[0];
          tb.select();
        }



        /*填充仓位*/
        function ChoosePosition(tb)
         {
            var date=new Date();
            var time=date.getTime();
            var par = tb.parentNode.parentNode;
            var wscode=par.getElementsByTagName("td")[21].getElementsByTagName("span")[1].innerHTML;
            var mcode=par.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time+"&&mcode="+mcode, '', "dialogHeight: 600px; dialogWidth: 1000px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                tb.style.background="white"; 
                var par = tb.parentNode.parentNode;
                var positioncode = par.getElementsByTagName("td")[22].getElementsByTagName("input")[1];
                positioncode.value = val[0];
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

                     var warehouse = gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("span")[0].innerHTML;
                     
                     var position=gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value;
                     
                    if (num == 0) {
                        gv1.rows[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].style.color = "#FF0000";
                       
                        flag=false;
                        alert("请填写正确的入库数量！");
                        break;
                    }
                    if (warehouse == "--请选择--") 
                    {
                        gv1.rows[i].getElementsByTagName("td")[21].getElementsByTagName("span")[0].style.color = "#FF0000";
                        
                        flag=false;
                        alert("仓库未填写！");
                    }
                    if (position == "待查"||position == "") 
                    {
                        gv1.rows[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].style.color = "#FF0000";
                        
                        flag=false;
                        alert("仓位未填写！");
                    }
                }
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
        
        
        function btnPrint_onclick(operate) {
        var date=new Date();
        var time=date.getTime();
        window.showModalDialog("SM_WarehouseIn_Print.aspx?incode=<%=LabelCode.Text %>&&id="+time+"&&oper="+operate,'',"dialogWidth=1200px;dialogHeight=580px;status=no;help=no;");   
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
   var  table=document.getElementById('<%= GridView1.ClientID %>');
   var tr=table.getElementsByTagName("tr");
    for(var i=1;i<tr.length;i++)
    {
     if(tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0]!=null)
       {
        tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked=true;
        tr[i].style.backgroundColor = "#adadad";
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

        } 
        else 
        { 
           obj.parentNode.parentNode.style.backgroundColor='#EFF3FB'; 

        } 
      }  
      
       
        function viewCondition(){
             document.getElementById("<%=PanelSplitLineHandle.ClientID%>").style.display='block';}   
        function clickalert(tb){
           var ischeck=confirm("单据将会结转到下期，这个功能只有当系统处于结账状态时，才能使用！请慎用！");
           if(ischeck)
           {
           tb.checked=true;
           }
           else
           {
           tb.checked=false;
           }
}   

     function ClientSideClick(myButton) {
     
           // Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            
            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                // diable the button
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
            
           return true;
        }
    
    </script>

    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> --%>
    <div class="box-inner">
        <div class="box_right">
           
                <table width="100%">
                    <tr>
                        <td align="left">
                            <input id="Close" type="button" value="关闭" onclick="closewin()" />
                            <asp:Button ID="Append" runat="server" Text="追加" OnClick="Append_Click"  OnClientClick="return append()" />
                            
                            <asp:Button ID="Split" runat="server" Text="拆分" CausesValidation="False" OnClientClick="viewCondition()" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSplit" runat="server" TargetControlID="Split"
                                PopupControlID="PanelSplitLine" Drag="True" PopupDragHandleControlID="PanelSplitLineHandle"
                                Enabled="True" X="500" Y="300" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="Delete" runat="server" Text="删除" CausesValidation="False" OnClick="Delete_Click" OnClientClick="return confirm('确认要删除吗？');"/>
                            
                            <asp:Button ID="Save" runat="server" Text="保存" CausesValidation="False" OnClick="Save_Click" />
                            
                            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />
                            
                            <asp:Button ID="FS" runat="server" Text="反审" OnClick="FS_Click"   OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False"   />
                            
                            <asp:Button ID="CD" runat="server" Text="拆单" OnClick="CD_Click" OnClientClick="return confirm('确定要拆分该入库单？');" />
                            
                            <asp:Button ID="HB" runat="server" Text="合并" OnClick="HB_Click" OnClientClick="return confirm('确定要合并该入库单？');" />
                            
                            <asp:Button ID="TH" runat="server" Text="下推红字" OnClick="TH_Click" OnClientClick="return confirm('确定要推红单？');" />
                            
                            <asp:Button ID="HX" runat="server" Text="核销" OnClick="HX_Click" OnClientClick="return confirm('确定要核销？');" />
                            
                            <asp:Button ID="FHX" runat="server" Text="反核销" OnClick="FHX_Click" OnClientClick="return confirm('确定要反核销？');" />
                            
                            <asp:Button ID="DelForm" runat="server" Text="删单" OnClick="DelForm_Click" OnClientClick="return delconfirm();" />
                            
                            <asp:Button ID="GL" runat="server" Text="关联单据" OnClick="GL_Click" Visible="false" />
                            <asp:Button ID="SumPrint" runat="server" Text="汇总打印" OnClick="SumPrint_Click" />
                            
                            <asp:Button ID="Print" runat="server" Text="明细打印" OnClick="Print_Click"/>
                            
                           <%-- <input id="btnSumPrint" runat="server" type="button" value="汇总打印" onclick="return btnPrint_onclick('sum')" />
                            <input id="btnPrint" runat="server" type="button" value="明细打印" onclick="return btnPrint_onclick('detail')" />--%>
                            <asp:Button ID="btnorder" runat="server" Text="到订单" OnClick="torder_Click" />
                            <asp:Button ID="btnbk" runat="server" Text="备库" OnClick="tobk_Click" Visible="false"  />
                            <asp:Button ID="btn_wxout" runat="server" Text="出库"  OnClick="wxout_Click" Visible="false" />
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <asp:Label ID="errorddsl" runat="server" ForeColor="Red"></asp:Label>
                    </tr>
                </table>
       
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
                        <asp:Button ID="ButtonSplitOK" runat="server" OnClick="ButtonSplitOK_Click" Text="确定"  />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonSplitCancel" runat="server" OnClick="ButtonSplitCancel_Click" 
                            Text="取消" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <div class="box-wrapper">
        <div class="box-outer">
             <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="计划跟踪号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="物料编码" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox3" runat="server" Text="物料名称" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox4" runat="server" Text="规格型号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox5" runat="server" Text="材质" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox6" runat="server" Text="国标" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox7" runat="server" Text="批号" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox8" runat="server" Text="是否定尺" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="长(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="宽(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox11" runat="server" Text="张(支)数" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox12" runat="server" Text="标准件" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="HeadPanel" runat="server" Width="100%">
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: large; text-align: center;" colspan="3">
                            <asp:Label runat="server" ID="LabelTittle" Text="外购入库单" Font-Size="Large"></asp:Label> <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />
                            &nbsp;&nbsp;&nbsp;
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 20%;">
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode" runat="server" ></asp:Label>

                            <asp:Label ID="LabelState" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="LabelGJState" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="LabelHXState" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="LabelHSState" runat="server" Visible="False"></asp:Label>
                            
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                        </td>
                        <td align="left" style="width: 20%;">
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server" Enabled="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                TargetControlID="TextBoxDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="width: 22%;">
                            供应商：<asp:Label ID="LabelSupplier" runat="server"></asp:Label>
                            <input type="text" id="InputSupplierCode" style="display: none" runat="server" />
                        </td>
                        <td align="center" style="width: 18%;">
                            收料仓库 ：<asp:DropDownList ID="DropDownListWarehouse" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="DropDownListWarehouse_SelecedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 20%;">
                            收料仓位 ：
                            <asp:DropDownList ID="DropDownListPosition" runat="server" AutoPostBack="True" AppendDataBoundItems="true"
                                OnSelectedIndexChanged="DropDownListPosition_SelecedIndexChanged">
                                <asp:ListItem Value="0">待查</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 20%;">
                            货单编号：<asp:TextBox ID="TextBoxHDBH" runat="server"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 20%;">
                            &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：<asp:TextBox ID="TextBoxAbstract" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBoxDate" runat="server" Text="结转到下期" CssClass="checkBoxCss" onclick="clickalert(this);" />
                        </td>
                        <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBoxPrint" runat="server" Text="打印标识" CssClass="checkBoxCss"/>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody" runat="server" Style="margin:0 auto 0 auto" Width="99%" Height="300px">
                <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    ShowFooter="True" EmptyDataText="没有相关数据！" AllowSorting="true" OnRowDataBound="GridView1_RowDataBound"
                    OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PTC" HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelFoot" runat="server" Text="合计:"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialCode" HeaderText="物料编码">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                                <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UniqueID")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialName" HeaderText="物料名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialStandard" HeaderText="规格型号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Attribute" HeaderText="材质" HeaderStyle-Wrap="false">
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
                        <asp:TemplateField SortExpression="Fixed" HeaderText="是否定尺" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Length" HeaderText="长(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'
                                    Width="60px" onkeyup="grControlFocus(this)" onclick="getSelect(this)" onblur="checklength(this)"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'
                                    Width="60px" onkeyup="grControlFocus(this)" onblur="checkwidth(this)"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="批号"  HeaderStyle-Wrap="false" SortExpression="LotNumber">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'
                                    Width="120px" onkeyup="grControlFocus(this)" onblur="checkLotNum(this)"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实收数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RN")%>'
                                    Width="60px" onkeyup="grControlFocus(this)" onblur="checkRN(this)"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalNum" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="张(支)数" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxQuantity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>'
                                    Width="60px" onkeyup="grControlFocus(this)" onblur="checkQuantity(this)"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalQuantity" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单价" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnitPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
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
                                <asp:Label ID="LabelCTUP" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CTUP")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="金额" HeaderStyle-Wrap="false" FooterStyle-Wrap="false" SortExpression="Amount">
                            <ItemTemplate>
                                <input type="text" id="InputAmount" value='<%#DataBinder.Eval(Container.DataItem, "Amount")%>'
                                    readonly="readonly" runat="server" style="width: 60px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalAmount" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="含税金额" HeaderStyle-Wrap="false"  FooterStyle-Wrap="false" SortExpression="CTA">
                            <ItemTemplate>
                                <input type="text" id="InputCTA" value='<%#DataBinder.Eval(Container.DataItem, "CTA")%>'
                                    readonly="readonly" runat="server" style="width: 60px" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalCTA" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                    Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                    style="display: none" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'
                                    Width="60px" onclick="ChoosePosition(this)"></asp:TextBox>
                                <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                    style="display: none" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false" SortExpression="OrderCode">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderCode")%>'></asp:Label>
                                
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
                      
                    </Columns>
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="False" />
                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                </yyc:SmartGridView>
            </asp:Panel>
            <asp:Panel ID="FooterPanel" runat="server" Width="100%">
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;部&nbsp;&nbsp;&nbsp;门：<asp:Label ID="LabelDep" runat="server"></asp:Label>
                            <asp:Label ID="LabelDepCode" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;业务员：<asp:Label ID="LabelClerk" runat="server"></asp:Label>
                            <asp:Label ID="LabelClerkCode" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;收料员：
                             <asp:DropDownList ID="DropDownListAcceptance" runat="server">
                            </asp:DropDownList>
                            
                          <%--<asp:Label ID="LabelRecieveCode" runat="server" Visible="False"></asp:Label>                      
                          
                          <asp:TextBox ID="TextBoxReciever" runat="server" Width="100px"
                                        OnTextChanged="TextBoxReciever_TextChanged" AutoPostBack="true" 
                                        onclick="changeStaff(this);"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxReciever" FirstRowSelected="true"
                                        MinimumPrefixLength="1" ServiceMethod="getStaff" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionSetCount="30">
                                    </cc1:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="TextBoxNameRequiredFieldValidator" ControlToValidate="TextBoxReciever" runat="server" ErrorMessage="请填写收料人"></asp:RequiredFieldValidator>--%>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server"></asp:Label>
                            <asp:Label ID="LabelVerifierCode" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                        </td>
                        <td align="left" style="width: 25%;">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <asp:Label ID="lberrrorbottom" Font-Size="Larger" runat="server" ForeColor="Red"></asp:Label>
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

    
    
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

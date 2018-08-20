<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_WarehouseOUT_LL.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_WarehouseOUT_LL"
    Title="重机领料单" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </cc1:ToolkitScriptManager>
    <link href="StyleFile/superTables.css" rel="stylesheet" type="text/css" />
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
            width: 400px !important;
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
    function llddy(id)
   {   
    var result=showModalDialog('SMLL_PRINT.aspx?id='+id,'subpage','dialogWidth:800px;dialogHeight:400px;center:yes;help:no;resizable:no;status:no'); //打开模态子窗体,并获取返回值
    window.location.href = window.location.href;
   }
    
    
        function checkLength(tb) {
            var length = parseFloat(tb.value);
            if (isNaN(length) || length < 0) {
                alert("请输入正确的长度！");
                tb.value = 0;
            }
//             var par = tb.parentNode.parentNode;
//             var kucunlength = Math.round(par.getElementsByTagName("td")[10].getElementsByTagName("span")[0].innerHTML);
//             if(kucunlength<length)           
//             {
//                   alert("长度不能大于库存长度！");
//              }
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
            var dn = parseFloat(par.getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML);
            var de = parseFloat(par.getElementsByTagName("td")[24].getElementsByTagName("span")[0].innerHTML);
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
           
            if (isNaN(rn)) {
                alert("请输入正确的数量！");
                tb.value = dn;
                Statistic();
            }
//            if (de != '0') {
//                if ((color == '0') && (rn > de)) {
//                    alert("出库数量不能大于定额数量！");
//                    tb.value = de;
//                    Statistic();
//                }
//            }
            else {
                if ((color == '0') && (rn > dn)) {
                    alert("出库数量不能大于库存数量！");
                    tb.value = dn;
                    Statistic();
                }
          
            }

            if (rn == 0) {
                tb.value = de;
                Statistic();
            }
            if ((color == '0') && (rn < 0)) {
                tb.value = -rn;
                if (tb.value > de) {
                    tb.value = de;
                }
                Statistic();
            }
            if ((color == '1') && (rn > 0)) {
                tb.value = -rn;
                Statistic();
            }
             if((color == '1') && (parseFloat(tb.value) <de)){
            
               alert("红单数量不能多于原单数量！");
               
               tb.value = de;
               
               Statistic();
            }
             Statistic();
        }

    /*填充仓位*/
        function ChoosePosition(tb)
         {
            var date=new Date();
            var time=date.getTime();
            var par = tb.parentNode.parentNode;
            var wscode=par.getElementsByTagName("td")[20].getElementsByTagName("span")[0].innerHTML;
            var mcode=par.getElementsByTagName("td")[3].getElementsByTagName("span")[0].innerHTML;
            val = window.showModalDialog("SM_WarehouseIN_PositionChoice.aspx?WSCODE=" + wscode+"&&id="+time+"&&mcode="+mcode, '', "dialogHeight: 600px; dialogWidth: 1000px; center: Yes; help: No; resizable: No; status: No;");
            if (val != null) 
            {
                tb.value = val[1];
                var positioncode = par.getElementsByTagName("td")[21].getElementsByTagName("input")[1];
                positioncode.value = val[0];
            }
        }

        function checkRQN(tb) {
            var rqn = parseFloat(tb.value);
            var par = tb.parentNode.parentNode;
            var dqn = Math.round(par.getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML);
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
         
//            if ((color == '1') && (parseFloat(tb.value)<dqn)) {
//                alert("红单数量不能大于原单数量！");   
//                tb.value = dqn;
//                Statistic();
//            }
            
          
            
            Statistic();
        }
        
        function Statistic() {
            var tdn = 0;
            var trn = 0;
            var tdqn = 0;
            var trqn = 0;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");

            for (i = 1; i < (gv1.rows.length - 1); i++) {
                var val1 = gv1.rows[i].getElementsByTagName("td")[14].getElementsByTagName("span")[0].innerHTML;
                tdn += parseFloat(val1);
                var val2 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                trn += parseFloat(val2);
                var val3 = gv1.rows[i].getElementsByTagName("td")[16].getElementsByTagName("span")[0].innerHTML;
                tdqn += parseFloat(val3);
                var val4 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                trqn += parseFloat(val4);
            }
            var lbtdn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[14].getElementsByTagName("span")[0];
            lbtdn.innerHTML = tdn.toFixed(4);
            var lbtrn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[15].getElementsByTagName("span")[0];
            lbtrn.innerHTML = trn.toFixed(4);
            var lbtdqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[16].getElementsByTagName("span")[0];
            lbtdqn.innerHTML = tdqn.toFixed(4);
            var lbtrqn = gv1.rows[gv1.rows.length - 1].getElementsByTagName("td")[17].getElementsByTagName("span")[0];
            lbtrqn.innerHTML = trqn.toFixed(4);
        }

        function checkPage() {

            var flag = true;
            var color = document.getElementById('<%= InputColour.ClientID %>').value;
            var sczh=document.getElementById('<%= TextBoxSCZH.ClientID %>').value;
            var gv1 = document.getElementById("<%=GridView1.ClientID %>");
              
             //仓库
        
            var ws = document.getElementById("<%=DropDownListDep.ClientID %>");
            
            var wscode = "";
            
            for (var i = 0; i < ws.options.length; i++)
            {
                if (ws.options[i].selected == true)
                {
                    wscode=ws.options[i].text;
                    
                    if(wscode=="--请选择--")
                    {
                       flag = false;
                    }
                    break;
                }
              
            }
            if (flag == false)
            {
              alert("请选择领料部门！");
              
              return flag;
              
            } 

            if (color == 0) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;//实发数量
                    var val2 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;//实发张支数
                    if (val1 <= 0) {
                        gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 <0) {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                }
                if (flag == false) {
                    
                    alert("蓝字出库单数量必须大于0！");
                    
                    return flag;
                } 
            }
            if (color == 1) {
                for (i = 1; i < (gv1.rows.length - 1); i++) {
                    var val1 = gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].value;
                    var val2 = gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value;
                    if (val1 >= 0) {
                        gv1.rows[i].getElementsByTagName("td")[15].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                    if (val2 >0) {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#FF0000";
                        flag = false;
                    }
                    else {
                        gv1.rows[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].style.color = "#0000FF";
                    }
                }
                if (flag == false) {
                    alert("红字出库单数量必须小于0！");
                   
                    return flag;
                } 
            }
            if(sczh=="")
            {
             flag = false;
            }
            
            if (flag == false) 
            {
                alert("生产制号不能为空！");
               
                return flag;
            } 
            return flag;
           
        }

        function append() {
            var date=new Date();
            var time=date.getTime();
            var retVal = window.showModalDialog("SM_Warehouse_Query.aspx?FLAG=APPENDOUT&&id="+time,"", "dialogWidth=1200px;dialogHeight=750px;status=no;help=no;scroll=no");
            return retVal;
        }

        function closewin() {
            window.opener.location = window.opener.location.href;
            window.close();
        } 
        
//          function btnPrint_onclick() {
//          
//          var date=new Date();
//        var time=date.getTime();
//        window.showModalDialog('SM_WarehouseOUT_Print.aspx?outcode=<%=LabelCode.Text %>&&id='+time,'',"dialogWidth=1000px;dialogHeight=580px;status=no;help=no;");   
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
        
    function tostorage() {
     var date=new Date();
        var time=date.getTime();
        window.open("SM_Warehouse_Query.aspx?FLAG=PUSHLLOUT&id="+time,'_self');
    }
    
//      function outstorage() {
//     var date=new Date();
//        var time=date.getTime();
//        window.open("SM_WarehouseOUT_LL_Auto.aspx?FLAG=PUSHBLUE&id="+time);
//    }
    
//    其他退库
   function backstorage() {
    var date=new Date();
        var time=date.getTime();
        window.open("SM_WarehouseOUT_Red.aspx?FLAG=PUSHRED&id="+time);
    }
     function viewCondition(){
   document.getElementById("<%=PanelSplitLineHandle.ClientID%>").style.display='block';
}   

//    var goodexit=false;
//    
//    function bindunbeforunload(){
//        goodexit=false;
//        window.onbeforeunload=perforresult;
//    }
//    
//    function unbindunbeforunload()
//    {
//        goodexit=true;
//        window.onbeforeunload=null;
//    }
//    
//    function perforresult()
//    {
//      if(!goodexit)
//      {
//        return "当前操作未保存，如果你此时离开，所做操作信息将全部丢失，是否离开?";
//      }
//    }

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

                                }
                            }
                        }  
                    }
                }
            }
        }
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
        function changeEngid(tb)
        {
          var engid=tb.value
//          tb.value=engid.split('-')[engid.split('-').length-1];
          tb.select();
        }
         function getEngid(tb)
        {
      
        tb.value=document.getElementById('<%= LabelSCZH.ClientID %>').innerHTML;
        }
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
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <input id="Close" type="button" value="关闭" onclick="closewin()" />
                            <asp:Button ID="Append" runat="server" Text="追加" OnClick="Append_Click" OnClientClick="return append()" />
                            <asp:Button ID="Split" runat="server" Text="拆分" OnClientClick="viewCondition()" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderSplit" runat="server" TargetControlID="Split"
                                PopupControlID="PanelSplitLine" Drag="True" PopupDragHandleControlID="PanelSplitLineHandle"
                                Enabled="True" X="500" Y="300" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />
                            <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" />
                            <asp:Button ID="Verify" runat="server" Text="审核" OnClick="Verify_Click" OnClientClick="ClientSideClick(this);"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="AntiVerify" runat="server" Text="反审核" OnClick="AntiVerify_Click"
                                OnClientClick="ClientSideClick(this);" UseSubmitBehavior="False" />
                            <asp:Button ID="SplitForm" runat="server" Text="拆单" OnClick="SplitForm_Click" OnClientClick="ClientSideClick(this);"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="MergeForm" runat="server" Text="合并" OnClick="MergeForm_Click" OnClientClick="ClientSideClick(this);"
                                UseSubmitBehavior="False" />
                            <asp:Button ID="DeleteBill" runat="server" Text="删单" OnClick="DeleteBill_Click" />
                            <asp:Button ID="PushRed" runat="server" Text="下推红字" OnClick="PushRed_Click" />
                            <asp:Button ID="AdjustLenWid" runat="server" Text="调整长宽" OnClick="AdjustLenWid_Click"
                                Visible="false" />
                            <asp:Button ID="BtnBackStorage" runat="server" Text="特殊退库" OnClick="BackStorage_Click" />
                            <asp:Button ID="Related" runat="server" Text="关联单据" OnClick="Related_Click" Visible="false" />
                            <%--<input id="btnPrint" runat="server" type="button" value="打印" onclick="return btnPrint_onclick()" />--%>
                            <asp:Button Visible="false" ID="SumPrint" runat="server" Text="汇总打印" OnClientClick="SumPrint()" />
                            <asp:Button Visible="false" ID="Print" runat="server" Text="明细打印" OnClientClick="DetailPrint()"/>
                            <asp:Button ID="Btn_daochu" runat="server" Text="导出Excel" OnClick="Btn_daochu_Click" />
                            <input id="btnstorge" type="button" value="到库存" onclick="tostorage()" runat="server" />
                            <%--<input id="OutStorage" type="button" value="生产领料单" onclick="outstorage()" runat="server" />--%>
                            
                            <asp:Button ID="ButtonSCHLL" runat="server" Text="生产领料单" OnClick="ButtonSCHLL_Click"  />
                            <asp:Button ID="copyform" runat="server" Text="单据头复制" OnClick="copyform_Click" Visible="false" />
                            <asp:Button ID="btn_mto" runat="server" Text="MTO" OnClick="btn_mto_Click" Visible="false" />
                            <asp:Button ID="tostorge" runat="server" Text="到库存" OnClick="tostorge_Click" CausesValidation="false"
                                Visible="false" />
                              
                        </td>
                        <td>
                            <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <asp:Label ID="errorBMorSL" runat="server" ForeColor="Red"></asp:Label>
                    </tr>
                </table>
            </div>
            <div>
                        <table width="98%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnprint" runat="server" Text="到打印页面" OnClick="btnprint_click" />&nbsp;&nbsp;&nbsp;
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
                        <asp:Button ID="ButtonSplitOK" runat="server" OnClick="ButtonSplitOK_Click" Text="确定" />
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
                        <asp:CheckBox ID="CheckBox9" runat="server" Text="库存长(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox10" runat="server" Text="长(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox11" runat="server" Text="宽(mm)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox12" runat="server" Text="即时库存张(支)" AutoPostBack="true"
                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox13" runat="server" Text="实发张(支)" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        <asp:CheckBox ID="CheckBox14" runat="server" Text="标准件" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td>
                            系统封账时间:<asp:Label ID="LabelClosingAccount" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="font-size: x-large; text-align: center;"  colspan="2">
                            重机领料单<asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" /> &nbsp;&nbsp;&nbsp;
                                
                                <asp:Image ID="ImageRed" runat="server" ImageUrl="~/Assets/images/red.gif" Visible="false" />
                        </td>
                       
                        <td >
                            &nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号&nbsp;&nbsp;&nbsp;：
                            <asp:Label ID="LabelCode" runat="server" Style="display: none"></asp:Label>
                            <asp:Label ID="LabelTrueCode" runat="server" Text='<%#Eval("TrueCode")%>'></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                            <asp:Label ID="LabelBillType" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>    
                    <td>                    
                        <td align="left" style="white-space: nowrap; width: 25%">
                            日&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtenderDate" runat="server" TargetControlID="TextBoxDate"
                                Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    &nbsp;&nbsp;&nbsp;任务号：<asp:TextBox ID="TextBoxSCZH" runat="server" Width="170px"
                                         AutoPostBack="true" onblur="getEngid(this)";"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="ac1" runat="server" TargetControlID="TextBoxSCZH" FirstRowSelected="true"
                                        MinimumPrefixLength="1" ServiceMethod="getTaskID" ServicePath="~/Ajax.asmx" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        CompletionSetCount="30">
                                    </cc1:AutoCompleteExtender>
                                    <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="TextBoxSCZH"
                                        PopupControlID="Panel1" OffsetX="0" OffsetY="-18">
                                    </cc1:HoverMenuExtender>
                                    <asp:Panel ID="Panel1" runat="server" Style="background-color: #79c8ef; visibility: hidden;">
                                        <asp:Label ID="LabelSCZH" runat="server" ForeColor="Black"></asp:Label>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%-- <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="TextBoxSCZH"></asp:RequiredFieldValidator>--%>
                        </td>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            领料部门：<asp:DropDownList ID="DropDownListDep" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="white-space: nowrap; width: 25%">
                            &nbsp;&nbsp;&nbsp;领料班组:
                            <asp:DropDownList ID="DropDownListBZ" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBoxComment" runat="server" Visible="false"></asp:TextBox>
                        </td>
                        <%--<td>
                            子项名称：
                            <asp:DropDownList ID="DropDownListZXMC" runat="server">
                                <asp:ListItem Value="--">-请选择-</asp:ListItem>
                                <asp:ListItem Value="包装">--包装--</asp:ListItem>
                                <asp:ListItem Value="支撑">--支撑--</asp:ListItem>
                                <asp:ListItem Value="窑托">--窑托--</asp:ListItem>
                                <asp:ListItem Value="磨托">--磨托--</asp:ListItem>
                                <asp:ListItem Value="备料">--备料--</asp:ListItem>
                                <asp:ListItem Value="底座">--底座--</asp:ListItem>
                                <asp:ListItem Value="预组装">--预组装--</asp:ListItem>
                                
                            </asp:DropDownList>
                           
                        </td>--%>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;张&nbsp;&nbsp;&nbsp;数：<asp:TextBox ID="TextBoxPageNum" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="2">
                           &nbsp;&nbsp;&nbsp;备&nbsp;&nbsp;&nbsp;注：
                        
                        <asp:TextBox ID="TextBoxNOTE1" runat="server" TextMode="MultiLine" Height="42px" Width="360px" ></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:CheckBox ID="chkxczf" runat="server" Text="现场直发" />
                        </td>
                    </tr>
                    <tr> 
                <td align="left" style="white-space: nowrap; width: 25%">
                 &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="CheckBoxDate" runat="server" Text="会计期间" CssClass="checkBoxCss" onclick="clickalert(this);"  />
                        
                    </td>
                    <td colspan="3">                      
                        
                    </td>
                 </tr>
                </table>
            </asp:Panel>
            <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>
            <asp:Panel ID="PanelBody" runat="server" Style="margin: 0 auto 0 auto" Width="99%"
                Height="300px">
                <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    EmptyDataText="没有相应的记录！" EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound"
                    OnDataBound="GridView1_DataBound" OnSorting="GridView1_Sorting" AllowSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                                <asp:Label ID="LabelUniqueID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"UniqueID")%>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PTC" HeaderText="计划跟踪号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPTC" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PTC")%>'
                                    Width="100%"></asp:Label>
                                <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" TargetControlID="LabelPTC"
                                    PopupControlID="Panel2" OffsetX="0" OffsetY="0">
                                </cc1:HoverMenuExtender>
                                <asp:Panel ID="Panel2" runat="server" Style="background-color: #A8B7EC; visibility: hidden;">
                                    <asp:Label ID="LabelDIFNUM" runat="server" ForeColor="Black" Text='<%#DataBinder.Eval(Container.DataItem, "DIFNUM")%>'
                                        Width="80px"></asp:Label>
                                </asp:Panel>
                            </ItemTemplate>
                            <FooterTemplate>
                                合计:
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialCode" HeaderText="物料代码" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelSQCODE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SQCODE")%>'
                                    Visible="false"></asp:Label>
                                <asp:Label ID="LabelMaterialCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialName" HeaderText="物料名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MaterialStandard" HeaderText="规格型号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelMaterialStandard" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "MaterialStandard")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Attribute" HeaderText="材质" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelAttribute" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Attribute")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="GB" HeaderText="国标" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelGB" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "GB")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LotNumber" HeaderText="批号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <%--<asp:Label ID="LabelLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>'></asp:Label>--%>
                                <asp:TextBox ID="TextBoxLotNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LotNumber")%>' Enabled="false"></asp:TextBox>                           
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否定尺" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelFixed" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Fixed")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="库存长(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DueLength")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="长(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxLength" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Length")%>'
                                    onblur="checkLength(this)" Width="60px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="宽(mm)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxWidth" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Width")%>'
                                    onblur="checkWidth(this)" Width="60px" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelUnit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Unit")%>'
                                    Width="30px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="即时库存数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelDN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DN")%>'
                                    Width="100%"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalDN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                            <FooterStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DNUM")%>'
                                    onblur="checkRN(this)" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="即时库存张(支)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelDQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DQN")%>'
                                    Width="100px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalDQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" HorizontalAlign="Right" />
                            <FooterStyle Wrap="false" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="实发张(支)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxRQN" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RQN")%>'
                                    onblur="checkRQN(this)" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="LabelTotalRQN" runat="server"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="计划模式" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelPlanMode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "PlanMode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单单号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发料仓库" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelWarehouseCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "WarehouseCode")%>'
                                    Style="display: none"></asp:Label>
                                <asp:Label ID="LabelWarehouse" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Warehouse")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxPosition" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Position")%>'
                                    Width="60px" onclick="ChoosePosition(this)" Enabled="false"></asp:TextBox>
                                <input type="text" id="InputPositionCode" value='<%#DataBinder.Eval(Container.DataItem, "PositionCode")%>'
                                    style="display: none" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标识号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelBSH" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BSH")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBoxComment" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comment")%>'
                                    Width="100px"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="定额数量" HeaderStyle-Wrap="false">
                            <ItemTemplate>
          
                                      <asp:Label ID="lblDNUM" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DNUM")%>'
                                    Width="100%"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="扫码唯一识别码" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="LabelOP_QRUniqCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "OP_QRUniqCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <RowStyle BackColor="#EFF3FB" Wrap="false" />
                    <FooterStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <EditRowStyle BackColor="#2461BF" />
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <FixRowColumn FixRowType="Header,Pager" TableWidth="100%" FixColumns="0,1,2,3,4,5,6" />
                </yyc:SmartGridView>
            </asp:Panel>
            <asp:Panel ID="FooterPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单：<asp:Label ID="LabelDoc" runat="server"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;发&nbsp;&nbsp;&nbsp;料：<asp:DropDownList ID="DropDownListSender"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 25%;">
                            &nbsp;&nbsp;&nbsp;审核人：<asp:Label ID="LabelVerifier" runat="server" Width="100px"></asp:Label>
                            <asp:Label ID="LabelVerifierCode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 25%;">
                            审核日期：<asp:Label ID="LabelApproveDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
           <asp:Label ID="lberrorbottom" Font-Size="Larger" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>

    <script type="text/javascript">
  
   var sDataTable=document.getElementById("<%=GridView1.ClientID %>")
  
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
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

    </script>

</asp:Content>

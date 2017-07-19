<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="EQU_GXHT.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_GXHT" Title="设备安全管理" %>

<%@ Register TagName="UCPaging" TagPrefix="uc1" Src="~/Controls/UCPaging.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    设备购销合同
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 80%;
            border: solid 1px #E5E5E5;
        }
        .tab tr
        {
            height: 30px;
        }
        .tab tr td
        {
            border: solid 1px #E5E5E5;
            border-collapse: collapse;
        }
    </style>
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
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
      
        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }

        function xr2() {
            $("#hidPerson").val("person2");
            SelPersons();
        }

        function xr3() {
            $("#hidPerson").val("person3");
            SelPersons();
        }
        function xr4() {
            $("#hidPerson").val("person4");
            SelPersons();
        }
        function xr5() {
            $("#hidPerson").val("person5");
            SelPersons();
        }
       

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=txt_HT_SHR1.ClientID %>").val(r.st_name);
            }
            if (id == "person2") {
                $("#<%=txt_HT_SHR2.ClientID %>").val(r.st_name);
            }
            if (id == "person3") {
                $("#<%=txt_HT_SHR3.ClientID %>").val(r.st_name);
            }
            if (id == "person4") {
                $("#<%=txt_HT_SHR4.ClientID %>").val(r.st_name);
            }
            if (id == "person5") {
                $("#<%=txt_HT_SHR5.ClientID %>").val(r.st_name);
            }
          
            $('#win').dialog('close');
        }
        
        function SumMoney(obj){
            var num = obj.value;
            var patten = /^(\+|\-)?[0-9][0-9]{0,9}(\.[0-9]{1,2})?$/;
            if (!patten.test(num)) {
                alert('请输入正确的数据格式！！！');
                obj.value = "0.00";
                obj.focus();
            }
           var money=0;
           $("#table1 .zz").each(
           function(){
           if($(this).val()!=""){
            money+=parseFloat($(this).val());
           }
           });
           $("#<%=txt_HT_HTZJ.ClientID %>").val(money);
           
            var n = $("#<%=txt_HT_HTZJ.ClientID%>").val();
            var unit = "千百拾亿千百拾万千百拾元角分", str = "";
            n += "00";
            var p = n.indexOf('.');
            if (p >= 0)
                n = n.substring(0, p) + n.substr(p + 1, 2);
            unit = unit.substr(unit.length - n.length);
            for (var i = 0; i < n.length; i++)
                str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);
            var a = str.replace(/零(千|百|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1").replace(/(亿)万|壹(拾)/g, "$1$2").replace(/^元零?|零分/g, "").replace(/元$/g, "元整");
            $("#<%=txt_HT_HTZJ.ClientID%>").val(a + "(" + money + "元)");
            
        }
        
          function CheckNum(obj) {
            var pattem = /^[0-9]*$/; //数量验证
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("请输入正确的数值！！！");
                obj.value = "1";
            }
        }
         $(document).ready(function() {
            var inputs = document.getElementById('<%=TabPanel1.ClientID%>').getElementsByTagName("input");
            var a = 0;
            for (var i = 0, length = inputs.length; i < length; i++) {
                if ((inputs[i].type == "text" && inputs[i].id == "ctl00_PrimaryContent_TabContainer1_TabPanel1_txt_HT_QDSJ")||(inputs[i].type == "text" && inputs[i].id == "ctl00_PrimaryContent_TabContainer1_TabPanel1_txt_HT_JHSJ")||(inputs[i].type == "text" && inputs[i].id == "ctl00_PrimaryContent_TabContainer1_TabPanel1_txt_HT_QYSJ")) {
                    a = i + 1;
                    inputs[a].readOnly = true;
                }
            }
            inputs[a].readOnly = true;
        })
        
        function txtName_TextChanged(obj){
        $("#<%=txt_HT_QYSJ1 %>").val(obj.value);
        }
        
        function dai() {
          var provider = $("#<%=txt_HT_GF.ClientID%>").val();
          if (provider != "") {
              if (provider.indexOf('|') > 0) {
                  var str = provider.split('|');
                  $("#<%=txt_HT_GF.ClientID%>").val(str[0]);
                  $("#<%=txt_HT_DWMC.ClientID%>").val(str[0]);
                  $("#<%=txt_HT_FDDBR.ClientID%>").val(str[1]);
                  $("#<%=txt_HT_KHYH.ClientID%>").val(str[2]);
                  $("#<%=txt_HT_ZH.ClientID%>").val(str[3]);
                  $("#<%=txt_HT_DH.ClientID%>").val(str[4]);
                  $("#<%=txt_HT_CZ.ClientID%>").val(str[5]);
                  $("#<%=txt_HT_DZ.ClientID%>").val(str[6]);
                  $("#<%=txt_HT_YB.ClientID%>").val(str[7]);
              }
              else {
                  $("#<%=txt_HT_DWMC.ClientID%>").val(str[0]);
              }
          }
       }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div style="width: 100%;" align="right">
        <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_OnClick" Width="40px"
            Height="20px" />
        &nbsp;&nbsp;
        <asp:Button runat="server" ID="btnSubmit" Text="提交审批" OnClick="btnSubmit_OnClick"
            Visible="false" Width="55px" Height="20px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnBack" Text="返回" OnClick="btnBack_OnClick" Width="40px"
            Height="20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <asp:TabContainer runat="server" ID="TabContainer1">
        <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="购销合同">
            <HeaderTemplate>
                购销合同
            </HeaderTemplate>
            <ContentTemplate>
                <div id="ht" style="width: 100%" align="center">
                    <asp:Panel runat="server" ID="panGXHT">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="font-size: xx-large;">
                                    <strong>设&nbsp;备&nbsp;购&nbsp;销&nbsp;合&nbsp;同</strong>
                                </td>
                            </tr>
                        </table>
                        <table class="tab">
                            <tr>
                                <td style="width: 20%">
                                    买方（需方）：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label runat="server" ID="lb_XF" Text="中材（天津）重型机械有限公司"></asp:Label>
                                </td>
                                <td style="width: 20%">
                                    合同编号：
                                </td>
                                <td style="width: 30%">
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_HTBH" onfocus="this.blur()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    卖方（供方）：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_GF" onchange="dai()"></asp:TextBox>
                                    <asp:AutoCompleteExtender runat="server" ID="aceGYS" TargetControlID="txt_HT_GF"
                                        ServicePath="~/Practice/autocomplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetGYS_NAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td>
                                    签订时间：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_QDSJ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    一.产品名称、商标、型号、厂家、数量、金额、供货时间及数量&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label
                                        ID="lbNum" runat="server" Text="行数："></asp:Label>
                                    <asp:TextBox ID="txtNum" runat="server" ForeColor="Red" onblur="CheckNum(this);"
                                        Width="30px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnAdd" runat="server" BackColor="LightGreen" OnClick="btnAdd_OnClick"
                                        Text="增加行" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" Text="删除行" />
                                </td>
                            </tr>
                            <tr style="margin-top: 0px; width: 100%">
                                <td colspan="4" style="width: 100%">
                                    <table id="table1" style="width: 100%">
                                        <asp:Repeater ID="rptSHBXX" runat="server" OnItemDataBound="SHBXX_OnItemDataBound">
                                            <HeaderTemplate>
                                                <tr align="center" style="background-color: #48D1CC;">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>设备名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>设备分项名称、规格、型号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单位</strong>
                                                    </td>
                                                    <td>
                                                        <strong>数量</strong>
                                                    </td>
                                                    <td>
                                                        <strong>单价</strong>
                                                    </td>
                                                    <td>
                                                        <strong>合计金额</strong>
                                                    </td>
                                                    <td>
                                                        <strong>备注</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr onmouseout="mout(this)" onmouseover="mover(this)">
                                                    <td>
                                                        <asp:CheckBox ID="shbXuHao" runat="server" Text="<%# Convert.ToInt32(Container.ItemIndex +1) %>"
                                                            TextAlign="Right" Width="60px" />
                                                        <asp:HiddenField ID="hid_EQU_ID" runat="server" Value='<%#Eval("EQU_ID")%>' />
                                                        <%--设备序号--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_Type" runat="server" Text='<%#Eval("EQU_Type")%>'></asp:TextBox>
                                                        <%--设备名称--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_Name" runat="server" Text='<%#Eval("EQU_Name")%>' TextMode="MultiLine"
                                                            Width="200px"></asp:TextBox>
                                                        <%--设备分项名称、规格、型号--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_Unit" runat="server" Text='<%#Eval("EQU_Unit")%>' Width="100px"></asp:TextBox>
                                                        <%--单位--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_Num" runat="server" Text='<%#Eval("EQU_Num")%>' Width="100px"></asp:TextBox>
                                                        <%--数量--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_UPrice" runat="server" Text='<%#Eval("EQU_UPrice")%>'></asp:TextBox>
                                                        <%--单价--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_TMoney" runat="server" CssClass="zz" onblur="SumMoney(this)"
                                                            Text='<%#Eval("EQU_TMoney")%>'></asp:TextBox>
                                                        <%--合计金额--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_EQU_Note" runat="server" Text='<%#Eval("EQU_Note")%>' TextMode="MultiLine"></asp:TextBox>
                                                        <%--备注--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    总金额：
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_HTZJ" runat="server" Height="90%" onfocus="this.blur()" Width="92%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    交货时间：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_JHSJ" onfocus="this.blur()" class="easyui-datebox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 100px;">
                                <td align="left" colspan="4">
                                    <asp:TextBox ID="txt_HT_TK" runat="server" Height="100%" TextMode="MultiLine" Width="99.6%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    需方
                                </td>
                                <td align="center" colspan="2">
                                    供方
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    单位名称：
                                </td>
                                <td>
                                    中材（天津）重型机械有限公司
                                </td>
                                <td>
                                    单位名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_DWMC" runat="server" Height="90%" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    法人代表：
                                </td>
                                <td>
                                    彭明德
                                </td>
                                <td>
                                    法定代表人：
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_FDDBR" runat="server" Height="90%" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    授权（委托）人签字：
                                </td>
                                <td>
                                </td>
                                <td>
                                    授权（委托）人签字：
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    签约时间：
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_QYSJ" runat="server" class="easyui-datebox" onchange="txtName_TextChanged(this)"></asp:TextBox></span>
                                </td>
                                <td>
                                    签约时间：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_QYSJ1" onfocus="this.blur()"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    开户银行：
                                </td>
                                <td>
                                    工商银行天津市财楼支行
                                </td>
                                <td>
                                    开户银行：
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_HT_KHYH" runat="server" Height="90%" Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    账 号：
                                </td>
                                <td>
                                    0302035309102231877
                                </td>
                                <td>
                                    账 号：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txt_HT_ZH" Width="90%" Height="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    电 话：
                                </td>
                                <td>
                                    022-86890126
                                </td>
                                <td>
                                    电 话：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_DH"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    传 真：
                                </td>
                                <td>
                                    022-86890156
                                </td>
                                <td>
                                    传 真：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_CZ"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    地 址：
                                </td>
                                <td>
                                    天津市津围公路北辰科技园区东扩区华泰道与景云路交口
                                </td>
                                <td>
                                    地 址：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_DZ"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    邮 编：
                                </td>
                                <td>
                                    300400
                                </td>
                                <td>
                                    邮 编：
                                </td>
                                <td>
                                    <asp:TextBox runat="server" Width="90%" Height="90%" ID="txt_HT_YB"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="TabPane2" HeaderText="审批信息">
            <ContentTemplate>
                <div style="width: 100%" align="center">
                    <asp:Panel runat="server" ID="panSP" Width="80%">
                        <asp:Panel runat="server" ID="tb">
                            <table width="80%">
                                <tr style="display: none">
                                    <td align="right" id="tdSPLX">
                                        <asp:RadioButtonList ID="rbl_HT_SPLX" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                            onchange="rbl_HT_SPLX_rbl_HT_SPLX()">
                                            <asp:ListItem Text="一级审核" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="三级审核" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="会审" Value="4" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Image runat="server" ID="Image2" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                        设备购销合同审批
                                        <asp:Image ID="ImageAUDIT" runat="server" ImageUrl="~/Assets/images/shenhe.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb0">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:HiddenField runat="server" ID="hid_HT_ZDRID" />
                                <asp:HiddenField runat="server" ID="hid_HT_SHR1ID" />
                                <asp:HiddenField runat="server" ID="hid_HT_SHR2ID" />
                                <asp:HiddenField runat="server" ID="hid_HT_SHR3ID" />
                                <asp:HiddenField runat="server" ID="hid_HT_SHR4ID" />
                                <asp:HiddenField runat="server" ID="hid_HT_SHR5ID" />
                                <tr style="height: 25px">
                                    <td align="center" style="width: 20%">
                                        制单人：&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="lb_HT_ZDR"></asp:Label>
                                    </td>
                                    <td>
                                        制单时间：&nbsp;&nbsp;
                                        <asp:Label runat="server" ID="lb_HT_ZDSJ"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        制单人建议:<br />
                                        <asp:TextBox ID="txt_HT_ZDR_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb1">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr style="height: 25px">
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR1" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR1" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR1_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR1_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR1_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb2">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR2" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR2" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR2_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR2_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR2_JY" runat="server" Height="42px" TextMode="MultiLine"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb3">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR3" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="imgSHR3" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR3_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR3_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR3_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb4">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR4" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image4" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr4()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR4_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR4_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR4_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="tb5">
                            <table align="center" width="80%" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <tr>
                                    <td align="center" style="width: 10%">
                                        审批人
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txt_HT_SHR5" runat="server" onfocus="this.blur()" Width="120px">
                                        </asp:TextBox>
                                        <asp:Image runat="server" ID="Image8" ImageUrl="../Assets/images/username_bg.gif"
                                            onclick="xr5()" align="middle" Style="cursor: pointer" title="选择" />
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核结论
                                    </td>
                                    <td align="center" style="width: 20%">
                                        <asp:RadioButtonList ID="rbl_HT_SHR5_JL" RepeatColumns="2" runat="server" Height="20px">
                                            <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="center" style="width: 10%">
                                        审核时间
                                    </td>
                                    <td style="width: 20%">
                                        <asp:Label ID="lb_HT_SHR5_SJ" runat="server" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        审批建议：<br />
                                        <asp:TextBox ID="txt_HT_SHR5_JY" runat="server" TextMode="MultiLine" Width="100%"
                                            Height="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_EatApplyDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_EatApplyDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    用餐申请及审批&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <%--    <script type="text/javascript">
p.c1{ width:300px;word-break:normal;border:1px solid red;}</script>--%>

    <script type="text/javascript" language="javascript">
        function shuru() {
            var i;
            var table = document.getElementById("tab");
            var tr = table.getElementsByTagName("tr");
            var objstr = '';
            for (i = 6; i < tr.length - 1; i++) {
                var obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];

                if (obj.checked) {
                    objstr = "checked";
                    break;
                }
            }
            if (objstr != "checked") {
                alert("请指定要插入行的位置!");
                return false;
            }
        }
        function checking() {
            var table = document.getElementById("tab");
            var tr = table.getElementsByTagName("tr");
            var count = 0;

            for (var i = 6; i < tr.length - 1; i++) {
                obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (obj.checked) {
                    count = i;
                    break;
                }
            }
            if (count > 0) {
                var i = confirm('确定删除吗？');
                if (i == true) {
                    return true;
                }
                else {
                    for (var i = 6; i < tr.length - 1; i++) {
                        obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                        if (obj.checked) {
                            obj.checked = false;
                        }
                    }
                    return false;
                }
            }
            else {
                alert("请选择要删除的行！");
                return false;
            }
        }
        function xiaoji(obj) {
            var xiaoji;
            var shuliang = $(obj).parent().parent().find("input[name*='num']").val();
            var danjia = $(obj).parent().parent().find("input[name*='price']").val();

            if (shuliang != "" && danjia != "") {
                xiaoji = danjia * shuliang;
                $(obj).parent().parent().find("input[name*='moneyone']").val(xiaoji);
            }
            else {
                xiaoji = 0;
                $(obj).parent().parent().find("input[name*='moneyone']").val(xiaoji);
            }
            moneyall(obj);
        }
        function moneyall(obj) {
            var i;
            var sum = 0;
            var table = document.getElementById("tab");
            var tr2 = document.getElementById("tr2");
            var tr = table.getElementsByTagName("tr");
            for (i = 7; i < tr.length - 1; i++) {
                var ss = tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
                console.log(ss);
                if (ss == "") {
                    ss = 0;
                }
                sum = parseFloat(ss) + sum;
            }
            var GUIGE = tr[3].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            if (GUIGE == "")
            { GUIGE = 0; }
            if (isNaN(GUIGE))
            { alert("用餐标准必须为数字！！"); }

            //  console.log(GUIGE);
            var RENSHU = tr[3].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
            if (RENSHU == "")
            { RENSHU = 0; }
            if (isNaN(RENSHU))
            { alert("用餐人数必须为数字！！"); }
            if (!isNaN(GUIGE) && !isNaN(RENSHU)) {
                var SUM1 = parseInt(GUIGE) * parseInt(RENSHU);
                var SUM2 = SUM1 + sum;
                var ss = tr.length - 1;
                tr[ss].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = SUM2;
            }
        }
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            
            if (id == "first") {
                var table = document.getElementById("tab");
//                console.log(table);
                var tr = table.getElementsByTagName("tr");
                //                console.log(tr[2].getElementsByTagName("td"));

                
                tr[2].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value = r.st_name;
                tr[2].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = r.st_id;
            }
            $('#win').dialog('close');
        }



        function shuru2() {
            var i;
            var table = document.getElementById("tab2");
            var tr = table.getElementsByTagName("tr");
            var objstr = '';
            for (i = 6; i < tr.length - 1; i++) {
                var obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];

                if (obj.checked) {
                    objstr = "checked";
                    break;
                }
            }
            if (objstr != "checked") {
                alert("请指定要插入行的位置!");
                return false;
            }
        }
        function checking2() {
            var table = document.getElementById("tab2");
            var tr = table.getElementsByTagName("tr");
            var count = 0;

            for (var i = 6; i < tr.length - 1; i++) {
                obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (obj.checked) {
                    count = i;
                    break;
                }
            }
            if (count > 0) {
                var i = confirm('确定删除吗？');
                if (i == true) {
                    return true;
                }
                else {
                    for (var i = 6; i < tr.length - 1; i++) {
                        obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                        if (obj.checked) {
                            obj.checked = false;
                        }
                    }
                    return false;
                }
            }
            else {
                alert("请选择要删除的行！");
                return false;
            }
        }
        function xiaoji2(obj) {
            var xiaoji;
            var shuliang = $(obj).parent().parent().find("input[name*='num2']").val();
            var danjia = $(obj).parent().parent().find("input[name*='price2']").val();

            if (shuliang != "" && danjia != "") {
                xiaoji = danjia * shuliang;
                $(obj).parent().parent().find("input[name*='moneyone2']").val(xiaoji);
            }
            else {
                xiaoji = 0;
                $(obj).parent().parent().find("input[name*='moneyone2']").val(xiaoji);
            }
            moneyall2(obj);
        }
        function moneyall2(obj) {
            debugger;
            var i;
            var sum = 0;
            var table = document.getElementById("tab2");
            var tr2 = document.getElementById("tr2");
            var tr = table.getElementsByTagName("tr");
            for (i = 6; i < tr.length - 1; i++) {
                var ss = tr[i].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value;
                console.log(ss);
                if (ss == "") {
                    ss = 0;
                }
                sum = parseFloat(ss) + sum;
            }

            var GUIGE = tr[2].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value;
            if (GUIGE == "")
            { GUIGE = 0; }
            if (isNaN(GUIGE))
            { alert("用餐标准必须为数字！！"); }
            console.log(GUIGE);
            var RENSHU = tr[2].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value;
            if (RENSHU == "")
            { RENSHU = 0; }
            if (isNaN(RENSHU))
            { alert("用餐人数必须为数字！！"); }
            if (!isNaN(GUIGE) && !isNaN(RENSHU)) {
                var SUM1 = parseInt(GUIGE) * parseInt(RENSHU);
                var SUM2 = SUM1 + sum;
                var ss = tr.length - 1;
                tr[ss].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value = SUM2;
            }
            //  var SUM1;
            //  if(GUIGE!=""&&RENSHU!="")
            //  {
            //  SUM1=parseInt(GUIGE)*parseInt(RENSHU);
            //  var SUM2=SUM1+sum;
            //  var ss=tr.length-1;
            //  tr2[ss].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=SUM2;
            //  }
            //  else
            //  {
            //  var ss=tr.length-1;
            //  tr2[ss].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=sum;
            //  }

        }
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right ">
                    <div class="box-title">
                    <%--<asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>--%>
                        <table style="width: 98%">
                            <tr>
                                <td style="width: 20%" align="right">
                                    <asp:Label id="lab_caozuo" Text="操作：" runat="server" />
                                </td>
                                <td style="width: 40%" align="left">
                                    <asp:Button ID="btninsert" runat="server" ToolTip="插入选择行下方,不超过10条！！！" Text="增加行"
                                        OnClientClick="return shuru()" OnClick="btninsert_Click" BackColor="#99ccff" />
                                    <%--<input id="i" runat="server" type="button" title="插入" style="width:100px;" />--%>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btndelete" runat="server" Text="删除行" OnClientClick="return checking()"
                                        OnClick="btndelete_Click" BackColor="#99ccff" />
                                </td>
                                <td style="width: 20%" align="center">
                                    <asp:Button ID="btnLoad" runat="server" Visible="false" Text="保 存" OnClick="btnLoad_OnClick"
                                        BackColor="#99ccff" />
                                </td>
                                <td style="width: 20%" align="center">
                                    <asp:Button ID="btnReturn" runat="server" Text="返 回" OnClick="btnReturn_OnClick"
                                        BackColor="#99ccff" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <cc1:TabPanel ID="Tab_sqnr" runat="server" HeaderText="申请内容" TabIndex="0" Width="100%">
                    <ContentTemplate>
                        <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    申请日期
                                </td>
                                <td align="center">
                                    <input id="date" runat="server" class="easyui-datetimebox" editable="false" readonly="readonly" />
                                </td>
                                <td align="center">
                                    单号
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtCode" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐类型
                                </td>
                                <td align="center">
                                    <asp:RadioButtonList ID="rad_yctype" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text="加班餐" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="客饭" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    申请人
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_apply" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txt_apply_id" runat="server" Visible="false" />
                                </td>
                                <td align="center">
                                    申请人电话
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_phone" runat="server"></asp:TextBox>
                                </td>
                                <td align="center">
                                    部门负责人审批：
                                </td>
                                <td align="center">
                                    <input id="txt_first_fzr" type="text" name="txt_first_fzr" runat="server" readonly="readonly" />
                                    <%--<asp:TextBox ID="txt_first_fzr"  runat="server" Enabled="false"></asp:TextBox>--%>
                                    <font color="#ff0000">*</font>
                                    <%-- <asp:DropDownList ID="ddl_fzr" runat="server" Width="80%"></asp:DropDownList>--%>
                                </td>
                                <td>
                                    <input id="firstid_fzr" type="text" name="firstid_fzr" runat="server" readonly="readonly"
                                        style="display: none" />
                                    <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        选择
                                    </asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    用餐标准
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_guige" runat="server" onkeyup="moneyall(this)"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐人数
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_renshu" runat="server" onkeyup="moneyall(this)"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐时间
                                </td>
                                <td align="center">
                                    <input id="usetime1" name="usetime" runat="server" class="easyui-datetimebox" editable="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="1">
                                    <asp:Label ID="title" runat="server" Text="备注"></asp:Label>
                                </td>
                                <td align="center" colspan="1" style="color: Red">
                                    注：加班人员用餐须注明
                                    <br />
                                    用餐人员姓名及班组 <br />
                                    外来人员必须注明来访 <br />单位名称及客人姓名
                                </td>
                                <td align="center" colspan="5">
                                    <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="7">
                                    <asp:Label ID="Label1" runat="server" Text="饮品申请明细"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    序号
                                </td>
                                <td align="center" colspan="1">
                                    饮品名称
                                </td>
                                <td align="center" colspan="1">
                                    品种
                                </td>
                                <td align="center" colspan="1">
                                    数量
                                </td>
                                <td align="center" colspan="1">
                                    单位
                                </td>
                                <td align="center" colspan="1">
                                    单价
                                </td>
                                <td align="center" colspan="1">
                                    小计
                                </td>
                            </tr>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="tr2">
                                        <td align="center" style="width: 40px;">
                                            <%#Container.ItemIndex + 1%>&nbsp;<asp:CheckBox ID="cbchecked" CssClass="checkBoxCss"
                                                BorderStyle="None" runat="server"></asp:CheckBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsname" runat="server" type="text" value='<%#Eval("GOODNAME") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsguige" runat="server" type="text" value='<%#Eval("GOODGUIGE") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsnum" runat="server" type="text" value='<%#Eval("GOODNUM") %>' name="num"
                                                onkeyup="xiaoji(this)" />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsdanwei" runat="server" type="text" value='<%#Eval("GOODDANWEI") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsprice" runat="server" type="text" value='<%#Eval("GOODPRICE") %>'
                                                name="price" onkeyup="xiaoji(this)" />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="moneyone" runat="server" type="text" value='<%#Eval("GOODMONEY") %>' name="xiaoji" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                    合计：
                                </td>
                                <td align="center" colspan="1">
                                    <input id="moneyall" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab_spxx" runat="server" HeaderText="审批信息" TabIndex="1" Width="100%">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <table width="100%">
                                    <tr>
                                        <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                            申请审批
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="zhaungtai" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="danhao" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr id="tr1" runat="server">
                                        <td align="center">
                                            部门
                                        </td>
                                        <td colspan="3">
                                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        审批人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_first" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                        <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        审核结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Enabled="false"
                                                            Height="20px">
                                                            <asp:ListItem Text="同意" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                            Width="100%" Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="lblState" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <%--//实际用餐--%>
            <div class="box-inner">
                <div class="box_right ">
                    <div class="box-title">
                        <table style="width: 98%">
                            <tr>
                                <td style="width: 20%" align="right">
                                    <asp:Label id="lab_caozuo2" Text="操作:" runat="server" />
                                </td>
                                <td style="width: 40%" align="left">
                                    <asp:Button ID="btninsert2" runat="server" ToolTip="插入选择行下方,不超过10条！！！" Text=" 插 入"
                                        OnClientClick="return shuru2()" OnClick="btninsert2_Click" BackColor="#99ccff" />
                                    <%--<input id="i" runat="server" type="button" title="插入" style="width:100px;" />--%>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btndelete2" runat="server" Text="删 除" OnClientClick="return checking2()"
                                        OnClick="btndelete2_Click" BackColor="#99ccff" />
                                </td>
                                <td style="width: 20%" align="center">
                                    <asp:Button ID="btnLoad2" runat="server" Visible="false" Text="保 存" OnClick="btnLoad2_OnClick"
                                        BackColor="#99ccff" />
                                </td>
                                <td style="width: 20%" align="center">
                                    <asp:Button ID="btnReturn2" runat="server" Text="返 回" OnClick="btnReturn2_OnClick"
                                        BackColor="#99ccff" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer2" runat="server" Width="100%" TabStripPlacement="Top"
                ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="实际反馈内容" TabIndex="0" Width="100%">
                    <ContentTemplate>
                        <table id="tab2" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr>
                                <td align="center">
                                    反馈日期
                                </td>
                                <td align="center">
                                    <input id="date2" runat="server" class="easyui-datetimebox" editable="false" readonly="readonly" />
                                </td>
                                <td align="center">
                                    单号
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txtCode2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    食堂反馈人
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_apply2" runat="server" Enabled="false"></asp:TextBox>
                                     <asp:TextBox ID="txt_apply2_id" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    反馈人电话
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_phone2" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="center">
                                    部门申请人：
                                </td>
                                <td align="center">
                                    <input id="txt_first_fzr2" type="text" name="txt_first_fzr" runat="server" readonly="readonly" />
                                    <%--<asp:TextBox ID="txt_first_fzr"  runat="server" Enabled="false"></asp:TextBox>--%>
                                    <font color="#ff0000">*</font>
                                    <%-- <asp:DropDownList ID="ddl_fzr" runat="server" Width="80%"></asp:DropDownList>--%>
                                </td>
                                <td>
                                    <input id="firstid_fzr2" type="text" name="firstid_fzr" runat="server" readonly="readonly"
                                        style="display: none" />
                                    <%-- <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                    align="absmiddle" runat="server" />
                                                选择
                                            </asp:HyperLink>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    用餐标准
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_guige2" runat="server" onkeyup="moneyall2(this)"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐人数
                                </td>
                                <td align="center">
                                    <asp:TextBox ID="txt_renshu2" runat="server" onkeyup="moneyall2(this)"></asp:TextBox>
                                </td>
                                <td align="center">
                                    用餐时间
                                </td>
                                <td align="center">
                                    <input id="usetime22" name="usetime2" runat="server" class="easyui-datetimebox" editable="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="1">
                                    <asp:Label ID="title2" runat="server" Text="备注"></asp:Label>
                                </td>
                                <td align="center" colspan="6">
                                    <asp:TextBox ID="txt_contents2" runat="server" Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="7">
                                    <asp:Label ID="Label2" runat="server" Text="饮品申请明细"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    序号
                                </td>
                                <td align="center" colspan="1">
                                    饮品名称
                                </td>
                                <td align="center" colspan="1">
                                    品种
                                </td>
                                <td align="center" colspan="1">
                                    数量
                                </td>
                                <td align="center" colspan="1">
                                    单位
                                </td>
                                <td align="center" colspan="1">
                                    单价
                                </td>
                                <td align="center" colspan="1">
                                    小计
                                </td>
                            </tr>
                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="tr2">
                                        <td align="center" style="width: 40px;">
                                            <%#Container.ItemIndex + 1%>&nbsp;<asp:CheckBox ID="cbchecked" CssClass="checkBoxCss"
                                                BorderStyle="None" runat="server"></asp:CheckBox>
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsname2" runat="server" type="text" value='<%#Eval("GOODNAME") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsguige2" runat="server" type="text" value='<%#Eval("GOODGUIGE") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsnum2" runat="server" type="text" value='<%#Eval("GOODNUM") %>' name="num2"
                                                onkeyup="xiaoji2(this)" />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsdanwei2" runat="server" type="text" value='<%#Eval("GOODDANWEI") %>' />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="goodsprice2" runat="server" type="text" value='<%#Eval("GOODPRICE") %>'
                                                name="price2" onkeyup="xiaoji2(this)" />
                                        </td>
                                        <td align="center" colspan="1">
                                            <input id="moneyone2" runat="server" type="text" value='<%#Eval("GOODMONEY") %>'
                                                name="xiaoji2" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                </td>
                                <td align="center" colspan="1">
                                    合计：
                                </td>
                                <td align="center" colspan="1">
                                    <input id="moneyall2" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tabfk" Visible="false" runat="server" HeaderText="反馈确认" TabIndex="1"
                    Width="100%">
                    <ContentTemplate>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                                    border="1">
                                    <tr id="tr3" runat="server">
                                        <td align="center">
                                            部门
                                        </td>
                                        <td colspan="3">
                                            <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                                <tr style="height: 25px">
                                                    <td align="center" style="width: 10%">
                                                        反馈人
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_fankuiren" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                                                        <input id="txt_fankuirenid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                    </td>
                                                    <td align="center" style="width: 10%">
                                                        反馈结论
                                                    </td>
                                                    <td align="center" style="width: 20%">
                                                        <asp:RadioButtonList ID="rblfirst2" RepeatColumns="2" runat="server" Enabled="false"
                                                            Height="20px">
                                                            <asp:ListItem Text="同意" Value="8" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="不同意" Value="7"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <asp:TextBox ID="shenhenote3" Enabled="false" runat="server" TextMode="MultiLine"
                                                            Width="100%" Height="42px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="Label3" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
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
                            <input id="dep" name="dept" value="02"/>
                            <%--<input id="Text1" name="dept" value='<%=user_depid %>' data-options="disabled:true" />--%>
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
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
    
   
</asp:Content>

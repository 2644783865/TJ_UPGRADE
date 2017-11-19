<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_LZSXBLD_SZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_LZSXBLD_SZ" Title="无标题页" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    离职手续办理单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            border: solid 1px #E0EEE0;
            width: 80%;
            padding: 0;
            margin: 0;
        }
        .tab tr
        {
        }
        .tab tr td
        {
        }
    </style>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function SZ_SHR(){
            var tbxLZ_SCBZ=$("#<%=tbxLZ_SCBZ.ClientID %>").val();
            var tbxLZ_CGBZ=$("#<%=tbxLZ_CGBZ.ClientID %>").val();
            var tbxLZ_JSBZ=$("#<%=tbxLZ_JSBZ.ClientID %>").val();
            var tbxLZ_ZLBZ=$("#<%=tbxLZ_ZLBZ.ClientID %>").val();
            var tbxLZ_GCSBZ=$("#<%=tbxLZ_GCSBZ.ClientID %>").val();
            
            var tbxLZ_SCBBZ=$("#<%=tbxLZ_SCBBZ.ClientID %>").val();
            var tbxLZ_CWBZ=$("#<%=tbxLZ_CWBZ.ClientID %>").val();
            var tbxLZ_SBBZ=$("#<%=tbxLZ_SBBZ.ClientID %>").val();
            var tbxLZ_STJL=$("#<%=tbxLZ_STJL.ClientID %>").val();
            var tbxLZ_CKGLY=$("#<%=tbxLZ_CKGLY.ClientID %>").val();
            
            var tbxLZ_GKGLY=$("#<%=tbxLZ_GKGLY.ClientID %>").val();
            var txtLZ_GDZCGLY=$("#<%=txtLZ_GDZCGLY.ClientID %>").val();
            var tbxLZ_TSGLY=$("#<%=tbxLZ_TSGLY.ClientID %>").val();
            var tbxLZ_DWGLY=$("#<%=tbxLZ_DWGLY.ClientID %>").val();
            var txtLZ_DZSBGLY=$("#<%=txtLZ_DZSBGLY.ClientID %>").val();
            
            var tbxLZ_KQGLY=$("#<%=tbxLZ_KQGLY.ClientID %>").val();
            var tbxLZ_LDGXGLR=$("#<%=tbxLZ_LDGXGLR.ClientID %>").val();
            var tbxLZ_SXGLR=$("#<%=tbxLZ_SXGLR.ClientID %>").val();
            var tbxLZ_GJJGLR=$("#<%=tbxLZ_GJJGLR.ClientID %>").val();
            var tbxLZ_DAGLR=$("#<%=tbxLZ_DAGLR.ClientID %>").val();
            
            var tbxLZ_GRXXGLY=$("#<%=tbxLZ_GRXXGLY.ClientID %>").val();
            var tbxLZ_ZHBBZ=$("#<%=tbxLZ_ZHBBZ.ClientID %>").val();
            var tbxLZ_LD=$("#<%=tbxLZ_LD.ClientID %>").val();



            if (tbxLZ_SCBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_CGBZ.length==0) {
                alert('请选择审批人');
                return false;
                
            }
            if (tbxLZ_JSBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_ZLBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_GCSBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            
            if (tbxLZ_SCBBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_CWBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_SBBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_STJL.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_CKGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            
            if (tbxLZ_GKGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (txtLZ_GDZCGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_TSGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_DWGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (txtLZ_DZSBGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            
            if (tbxLZ_KQGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_LDGXGLR.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_SXGLR.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_GJJGLR.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_DAGLR.length==0) {
                alert('请选择审批人');
                return false;
            }
            
            if (tbxLZ_GRXXGLY.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_ZHBBZ.length==0) {
                alert('请选择审批人');
                return false;
            }
            if (tbxLZ_LD.length==0) {
                alert('请选择审批人');
                return false;
            }       
        }
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
        function xr6() {
            $("#hidPerson").val("person6");
            SelPersons();

        }

        function xr7() {
            $("#hidPerson").val("person7");
            SelPersons();
        }

        function xr8() {
            $("#hidPerson").val("person8");
            SelPersons();
        }
                function xr9() {
            $("#hidPerson").val("person9");
            SelPersons();

        }

        function xr10() {
            $("#hidPerson").val("person10");
            SelPersons();
        }

        function xr11() {
            $("#hidPerson").val("person11");
            SelPersons();
        }
                function xr12() {
            $("#hidPerson").val("person12");
            SelPersons();

        }

        function xr13() {
            $("#hidPerson").val("person13");
            SelPersons();
        }

        function xr14() {
            $("#hidPerson").val("person14");
            SelPersons();
        }
                function xr15() {
            $("#hidPerson").val("person15");
            SelPersons();

        }

        function xr16() {
            $("#hidPerson").val("person16");
            SelPersons();
        }
                function xr17() {
            $("#hidPerson").val("person17");
            SelPersons();

        }

        function xr18() {
            $("#hidPerson").val("person18");
            SelPersons();
        }

        function xr19() {
            $("#hidPerson").val("person19");
            SelPersons();
        }
        function xr20() {
            $("#hidPerson").val("person20");
            SelPersons();

        }

        function xr21() {
            $("#hidPerson").val("person21");
            SelPersons();
        }

        function xr22() {
            $("#hidPerson").val("person22");
            SelPersons();
        }

        function xr23() {
            $("#hidPerson").val("person23");
            SelPersons();
        }

        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {
                $("#<%=tbxLZ_SCBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_SCBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person2") {
                $("#<%=tbxLZ_CGBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_CGBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person3") {
                $("#<%=tbxLZ_JSBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_JSBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person4") {
                $("#<%=tbxLZ_ZLBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_ZLBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person5") {
                $("#<%=tbxLZ_GCSBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GCSBZID.ClientID%>").val(r.st_id);
            }
            
            if (id == "person6") {
                $("#<%=tbxLZ_SCBBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_SCBBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person7") {
                $("#<%=tbxLZ_CWBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_CWBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person8") {
                $("#<%=tbxLZ_SBBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_SBBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person9") {
                $("#<%=tbxLZ_STJL.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_STJLID.ClientID%>").val(r.st_id);
            }
            if (id == "person10") {
                $("#<%=tbxLZ_CKGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_CKGLYID.ClientID%>").val(r.st_id);
            }
            
            if (id == "person11") {
                $("#<%=tbxLZ_GKGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GKGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person12") {
                $("#<%=txtLZ_GDZCGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GDZCGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person13") {
                $("#<%=tbxLZ_TSGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_TSGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person14") {
                $("#<%=tbxLZ_DWGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_DWGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person15") {
                $("#<%=txtLZ_DZSBGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_DZSBGLYID.ClientID%>").val(r.st_id);
            }
            
            if (id == "person16") {
                $("#<%=tbxLZ_KQGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_KQGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person17") {
                $("#<%=tbxLZ_LDGXGLR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_LDGXGLRID.ClientID%>").val(r.st_id);
            }
            if (id == "person18") {
                $("#<%=tbxLZ_SXGLR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_SXGLRID.ClientID%>").val(r.st_id);
            }
            if (id == "person19") {
                $("#<%=tbxLZ_GJJGLR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GJJGLRID.ClientID%>").val(r.st_id);
            }
            if (id == "person20") {
                $("#<%=tbxLZ_DAGLR.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_DAGLRID.ClientID%>").val(r.st_id);
            }
            
            if (id == "person21") {
                $("#<%=tbxLZ_GRXXGLY.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_GRXXGLYID.ClientID%>").val(r.st_id);
            }
            if (id == "person22") {
                $("#<%=tbxLZ_ZHBBZ.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_ZHBBZID.ClientID%>").val(r.st_id);
            }
            if (id == "person23") {
                $("#<%=tbxLZ_LD.ClientID %>").val(r.st_name);
                $("#<%=hidLZ_LDID.ClientID%>").val(r.st_id);
            }

            $('#win').dialog('close');
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%" style="border-color: #e0eee0;">
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClientClick="retrun SZ_SHR()" OnClick="btnSubmit_OnClick" />
                        <asp:Button runat="server" ID="btnQuit" Text="取消(返回)" OnClick="btnQuit_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 100%; background-color: #F0F8FF; height: 500px; overflow: auto"
        align="center" id="div1">
        <table width="80%">
            <tr>
                <td align="center" style="width: 100%">
                    <asp:Image runat="server" ID="Image" ImageUrl="~/OM_Data/StyleFile/OM_ZCTOP.jpg" />
                </td>
            </tr>
            <tr>
                <td style="font-size: x-large;" align="center">
                    <strong>离职手续办理单</strong>
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="panZ">
            <asp:Panel runat="server" ID="panJBXX">
                <table width="80%" style="border: solid 1px #E0EEE0">
                    <tr>
                        <td colspan="4">
                            <asp:RadioButtonList runat="server" ID="rblLZ_LX" RepeatDirection="Horizontal">
                                <asp:ListItem Text="办公室人员" Value="0"></asp:ListItem>
                                <asp:ListItem Text="一线班组" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td colspan="4">
                            制单人：
                            <asp:Label runat="server" ID="lbLZ_ZDR"></asp:Label>
                            <asp:HiddenField runat="server" ID="hidLZ_ZDRID" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;制单时间：
                            <asp:Label runat="server" ID="lbLZ_ZDSJ"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所在部门
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLZ_BUMEN" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            姓名
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlLZ_PERSON" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td>
                            性别
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLZ_GENDER"></asp:TextBox>
                        </td>
                        <td>
                            班组
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_BANZU">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位名称
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_GANGWEI"></asp:TextBox>
                        </td>
                        <td>
                            入职时间
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_RUZHISJ" class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                            申请离职时间
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_SQLZSJ" class="easyui-datebox" editable="false"></asp:TextBox>
                        </td>
                        <td>
                            合同年限
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="tbxLZ_HTNX"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="panBBM">
                <table width="80%">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td colspan="2">
                            <strong>一.本部门工作交接</strong>
                            <asp:Label runat="server" ID="lb1" Text="(申请人所在部门)" ForeColor="#ffe486"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="tab" width="80%">
                    <tr>
                        <td colspan="3">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>1、工作方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GZJJZTY" Text="已交接完毕" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GZJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_GZWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_GZJJZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>2、办公用品、设备、工具等方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_WPJJZTY" Text="已交接完毕" />
                            包括<asp:TextBox runat="server" ID="tbxLZ_WPJJ"></asp:TextBox>等物品
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_WPJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_WPWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_WPJJZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>3、资料方面：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLJJZTY" Text="已交接完毕" />
                            包括<asp:TextBox runat="server" ID="tbxLZ_ZLJJ"></asp:TextBox>等
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLJJZTN" Text="未交接完毕" />
                            (原因<asp:TextBox runat="server" ID="tbxLZ_ZLWJJYY"></asp:TextBox>)
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 接手人：
                            <asp:TextBox runat="server" ID="tbxLZ_ZLZMR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>4、离职日期确认：</strong><br />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="tbxLZ_RIQI" class="easyui-datebox"
                                editable="false"></asp:TextBox><br />
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 直接领导：
                            <asp:TextBox runat="server" ID="tbxLZ_ZJLL" Width="60px" onfocus="this.blur()"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZJLLZT" RepeatDirection="Horizontal">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidLZ_ZJLLID" />
            <table width="80%">
                <tr style="background-color: #CDC8B1; height: 30px">
                    <td colspan="2">
                        <strong>二、其他部门业务终止确认</strong>
                    </td>
                </tr>
            </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>生产管理部（105室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCYWZZZTY" Text="业务已终止"  Enabled="false"/>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCYWZZZTN" Text="业务未终止" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_SCYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_SCBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_SCBZID" />
                            <asp:Image runat="server" ID="Image1" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr1()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SCBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>采购部（104室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CGYWZZZTY" Text="业务已终止" Enabled="false"/>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CGYWZZZTN" Text="业务未终止" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CGYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_CGBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_CGBZID" />
                            <asp:Image runat="server" ID="Image2" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr2()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_CGBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>技术部（203室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_JSYWZZZTY" Text="业务已终止" Enabled="false"/>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_JSYWZZZTN" Text="业务未终止"  Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_JSYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_JSBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_JSBZID" />
                            <asp:Image runat="server" ID="Image3" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr3()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_JSBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>质量部（204室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLYWZZZTY" Text="业务已终止" Enabled="false"/>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_ZLYWZZZTN" Text="业务未终止" Enabled="false" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_ZLYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_ZLBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_ZLBZID" />
                            <asp:Image runat="server" ID="Image4" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr4()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZLBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>工程师办公室（203室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GCSYWZZZTY"
                                Text="业务已终止" Enabled="false" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_GCSYWZZZTN"
                                Text="业务未终止" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_GCSYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_GCSBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GCSBZID" />
                            <asp:Image runat="server" ID="Image5" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr5()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_GCSBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>市场部（303室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCBYWZZZTY"
                                Text="业务已终止" Enabled="false"/>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_SCBYWZZZTN"
                                Text="业务未终止" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_SCBYWWZZYY" Enabled="false"> </asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_SCBBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_SCBBZID" />
                            <asp:Image runat="server" ID="Image6" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr6()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SCBBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>财务部（305室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CWYWZZZTY" Text="业务已终止" Enabled="false" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CWYWZZZTN" Text="业务未终止" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CWYWWZZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长<asp:TextBox runat="server" ID="tbxLZ_CWBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_CWBZID" />
                            <asp:Image runat="server" ID="Image7" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr7()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_CWBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>安全环保部（201室）</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1、劳保用品退回:
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPGZF" Text="工作服" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPAQM" Text="安全帽" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPGZX" Text="工作鞋" />
                            <asp:CheckBox runat="server" ID="cbxLZ_LBYPQTZT" Text="其他" />
                            <asp:TextBox runat="server" ID="tbxLZ_LBYPQT" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2、用品折旧费用： 安全帽<asp:TextBox runat="server" ID="tbxLZ_AQMZJFY" Enabled="false"></asp:TextBox>元，工作服、鞋<asp:TextBox
                                runat="server" ID="tbxLZ_GZFXZJFY" Enabled="false"></asp:TextBox>共计<asp:TextBox runat="server" ID="tbxLZ_ZJFYHJ" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                            <td align="left">
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 经手人（签字）：<asp:TextBox runat="server" ID="tbxLZ_JSR"></asp:TextBox>
                            </td>
                        </tr>--%>
                    <tr>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 部长：<asp:TextBox runat="server" ID="tbxLZ_SBBZ" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_SBBZID" />
                            <asp:Image runat="server" ID="Image8" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr8()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_SBBZZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
 
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>食堂业务</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="cbxLZ_CFQSZTY" Text="餐费已清算" Enabled="false"/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CFQSZTN" Text="餐费未清算"  Enabled="false"/>，欠费<asp:TextBox runat="server"
                                ID="tbxLZ_CFQK" Enabled="false"></asp:TextBox>元。
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 食堂经理(签字)：<asp:TextBox runat="server" ID="tbxLZ_STJL"
                                Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_STJLID" />
                            <asp:Image runat="server" ID="Image9" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr9()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_STJLZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

            <table>
                <tr>
                    <td>
                        备注说明：<br />
                        1、部门工作交接应及时、全面并有相关工作交接单，办公设备、设施需有相关交接或保管人签收保管；<br />
                        2、所涉及业务部门须请负责人审批；<br />
                        3、请在已确认项前打勾，以表示该工作已交接或者完结。
                    </td>
                </tr>
            </table>
            <table width="80%">
                <tr style="background-color: #CDC8B1; height: 30px">
                    <td colspan="2">
                        三、综合办公室终止确认
                    </td>
                </tr>
            </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>1、餐卡退还：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CKTHZTY" Text="是" Enabled="false"/>,餐补余额<asp:TextBox runat="server"
                                ID="tbxLZ_CBYE" Enabled="false"></asp:TextBox>元，收款人<asp:TextBox runat="server" ID="tbxLZ_CBSKR" Enabled="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_CKTHZTN" Text="否" />
                            （原因<asp:TextBox runat="server" ID="tbxLZ_CKWTHYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_CKGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_CKGLYID" />
                            <asp:Image runat="server" ID="Image10" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr10()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_CKGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>2、工卡、《员工手册》退还：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GKTHZTY" Text="是" Enabled="false" />
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GKTHZTN" Text="否"  Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_GKWTHYY" Enabled="false"></asp:TextBox>，丢失补办费<asp:TextBox
                                runat="server" ID="tbxLZ_GKDSPC" Enabled="false"></asp:TextBox>元）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GKGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GKGLYID" />
                            <asp:Image runat="server" ID="Image11" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr11()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GKGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>3、固定资产交接：</strong>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                runat="server" ID="btnGDZCZY" Text="固定资产转移" BackColor="LightGreen" Enabled="false"/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                    runat="server" ID="btnCKJL" Text="查看记录" BackColor="LightGreen" Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        style="white-space: normal" border="1">
                                        <asp:Repeater ID="rptGDZC" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格或参数</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>经办人</strong>
                                                    </td>
                                                    <td style="width: 180px">
                                                        <strong>转移原因</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%#Container.ItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                                            ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                                            ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                                            ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                                            ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lbdt1" ForeColor="Red"></asp:Label></asp:Panel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="txtLZ_GDZCGLY" onfocus="this.blur()" Width="60px"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GDZCGLYID" />
                            <asp:Image runat="server" ID="Image12" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr12()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_GDZCZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>4、住宿结算：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_TSZTY" Text="是" Enabled="false"/>
                            ,退宿日期<asp:TextBox runat="server" ID="tbxLZ_TSRQ" class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>
                            ，退回押金<asp:TextBox runat="server" ID="tbxLZ_TSTHYJ" Enabled="false"></asp:TextBox>
                            元，水电费<asp:TextBox runat="server" ID="tbxLZ_TSSDF" Enabled="false"></asp:TextBox>
                            元。
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_TSZTN" Text="否" Enabled="false"/>
                            （原因<asp:TextBox runat="server" ID="tbxLZ_WTSYY" Enabled="false"></asp:TextBox>）
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_TSGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_TSGLYID" />
                            <asp:Image runat="server" ID="Image13" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr13()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="right">
                            <asp:RadioButtonList runat="server" ID="rblLZ_TSGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>5、网络信息处理</strong>
                        </td>
                    </tr>
                    <%--  <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）电子设备：<asp:CheckBox runat="server" ID="cbxLZ_DZSBJSJ"
                                Text="计算机" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBXJ" Text="相机" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBYDYP" Text="移动硬盘" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBUP" Text="U盘" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBQTZT" Text="其他" />
                            <asp:TextBox runat="server" ID="tbxLZ_DZSBQT"></asp:TextBox><br />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBZTY" Text="收回" />
                            <asp:CheckBox runat="server" ID="cbxLZ_DZSBZTN" Text="转移" />
                            (接受人：<asp:TextBox runat="server" ID="tbxLZ_DZSBJSR"></asp:TextBox>)
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 网络信息：<asp:CheckBox runat="server" ID="cbxLZ_WLXXNWYX"
                                Text="内网邮箱" Enabled="false"/>
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXWWYX" Text="外网邮箱" Enabled="false"/>
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXXSZH" Text="信使账户" Enabled="false"/>
                            <asp:CheckBox runat="server" ID="cbxLZ_OAZH" Text="OA账户" Enabled="false"/>
                            <asp:CheckBox runat="server" ID="cbxLZ_WLXXQTZT" Text="其他" Enabled="false"/>
                            <asp:TextBox runat="server" ID="tbxLZ_WLXXQT" Enabled="false"></asp:TextBox>，全部注销。
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_DWGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_DZSBGLYID" />
                            <asp:Image runat="server" ID="Image14" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr14()" align="middle" Style="cursor: pointer" title="选择" />
                            <asp:RadioButtonList runat="server" ID="rblLZ_DWGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<strong>6、电子设备（非固定资产）交接：</strong>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                runat="server" ID="btnFGDZCZY" Text="非固定资产转移" BackColor="LightGreen" Enabled="false" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button
                                    runat="server" ID="btnCK1" Text="查看记录" BackColor="LightGreen" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                        style="white-space: normal" border="1">
                                        <asp:Repeater ID="rptFGDZC" runat="server">
                                            <HeaderTemplate>
                                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                    <td>
                                                        <strong>序号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>编号</strong>
                                                    </td>
                                                    <td>
                                                        <strong>名称</strong>
                                                    </td>
                                                    <td>
                                                        <strong>规格或参数</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>前存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用人</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用部门</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现使用时间</strong>
                                                    </td>
                                                    <td>
                                                        <strong>现存放地点</strong>
                                                    </td>
                                                    <td>
                                                        <strong>经办人</strong>
                                                    </td>
                                                    <td style="width: 180px">
                                                        <strong>转移原因</strong>
                                                    </td>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                                    <td>
                                                        <%#Container.ItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                                            ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                                            ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                                            ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                                            ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center">
                                        <asp:Label runat="server" ID="lbdt2" ForeColor="Red"></asp:Label></asp:Panel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="txtLZ_DZSBGLY" onfocus="this.blur()" Width="60px"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_DWGLYID" />
                            <asp:Image runat="server" ID="Image15" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr15()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_DZSBZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>7、考勤确认</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）截止日期：<asp:TextBox runat="server" ID="tbxLZ_KQJZRQ"
                                class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2）年休假请假情况：可休
                            <asp:TextBox runat="server" ID="tbxLZ_KQNJKJ" Enabled="false"></asp:TextBox>天，已休
                            <asp:TextBox runat="server" ID="tbxLZ_KQNJYX" Enabled="false"></asp:TextBox>天
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 考勤员：<asp:TextBox runat="server" ID="tbxLZ_KQGLY" Width="60px"
                                onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_KQGLYID" />
                            <asp:Image runat="server" ID="Image17" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr16()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_KQGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>8、劳动关系解除：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 1）退工办理：截止日期<asp:TextBox runat="server" ID="tbxLZ_TGRQ"
                                class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp 2)工资结算：截至日期<asp:TextBox runat="server" ID="tbxLZ_GZJSRQ"
                                class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_LDGXGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_LDGXGLRID" />
                            <asp:Image runat="server" ID="Image16" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr17()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_LDGXGLRZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>9、社险缴费截至：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_SXJZZTY" Text="截止日期至：" Enabled="false"/>
                            <asp:TextBox runat="server" ID="tbxLZ_SXJZRQ" class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_SXJZZTN" Text="否" Enabled="false" />
                            （原因
                            <asp:TextBox runat="server" ID="tbxLZ_SXWJZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_SXGLR" Width="60px" onfocus="this.blur()" ></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_SXGLRID" />
                            <asp:Image runat="server" ID="Image18" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr18()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_SXGLRZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>10、公积金缴费截止：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GJJJZZTY" Text="截止日期至：" Enabled="false"/>
                            <asp:TextBox runat="server" ID="tbxLZ_GJJJZRQ" class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GJJJZZTN" Text="否" Enabled="false"/>
                            （原因
                            <asp:TextBox runat="server" ID="tbxLZ_GJJWJZYY" Enabled="false"></asp:TextBox>）
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GJJGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GJJGLRID" />
                            <asp:Image runat="server" ID="Image19" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr19()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GJJGLRZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
 
                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>11、人事档案托管情况：</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_DAZRSJZT" Text="转入公司账户时间：" Enabled="false"/>
                            <asp:TextBox runat="server" ID="tbxLZ_DAZRSJ" class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_DABGJZSJZT" Text="保管截止日期："  Enabled="false"/>
                            <asp:TextBox runat="server" ID="tbxLZ_DABGJZSJ" class="easyui-datebox" editable="false" Enabled="false"></asp:TextBox>）
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="cbxLZ_DAWZR" Text="未转入" Enabled="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_DAGLR" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_DAGLRID" />
                            <asp:Image runat="server" ID="Image20" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr20()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_DAGLRZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <strong>12、个人信息移除</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GRKQXXYCZT" Text="考勤系统个人信息移除"  Enabled="false"/>；
                        </td>
                        <td>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <asp:CheckBox runat="server" ID="cbxLZ_GRXTXXYCZT" Text="人员信息表个人信息移除" Enabled="false"/>。
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            管理员：<asp:TextBox runat="server" ID="tbxLZ_GRXXGLY" Width="60px" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_GRXXGLYID" />
                            <asp:Image runat="server" ID="Image21" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr21()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblLZ_GRXXGLYZT" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="同意" Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td>
                            <strong>综合办公室意见</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSPJL" Text="审批结论："></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_ZHBSPZT" RepeatDirection="Horizontal"
                                Width="80%" RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Text="同意离职&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                                    Value="y"></asp:ListItem>
                                <asp:ListItem Text="不同意离职" Value="n"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbSPJY" Text="审批建议："></asp:Label><br />
                            <asp:TextBox runat="server" ID="tbxLZ_ZHBSPJY" TextMode="MultiLine" Width="98%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            部长：<asp:TextBox runat="server" ID="tbxLZ_ZHBBZ" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_ZHBBZID" />
                            <asp:Image runat="server" ID="Image22" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr22()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                    </tr>
                </table>

                <table class="tab">
                    <tr style="background-color: #CDC8B1; height: 30px">
                        <td>
                            <strong>公司领导意见</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbLDSPJL" Text="审批结论："></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:RadioButtonList runat="server" ID="rblLZ_LDSPZT" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Enabled="false">
                                <asp:ListItem Value="y" Text="同意离职&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"></asp:ListItem>
                                <asp:ListItem Value="n" Text="不同意离职"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lbLDSPJY" Text="审批建议："></asp:Label><br />
                            <asp:TextBox runat="server" ID="tbxLZ_LDSPJY" TextMode="MultiLine" Width="98%" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            总经理：<asp:TextBox runat="server" ID="tbxLZ_LD" onfocus="this.blur()"></asp:TextBox>
                            <input type="hidden" runat="server" id="hidLZ_LDID" />
                            <asp:Image runat="server" ID="Image23" ImageUrl="../Assets/images/username_bg.gif"
                                onclick="xr23()" align="middle" Style="cursor: pointer" title="选择" />
                        </td>
                    </tr>
                </table>

        </asp:Panel>
    </div>
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
                        <input id="dep" name="dept" value="07">
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
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;<a class="easyui-linkbutton"
                    data-options="iconCls:'icon-ok',plain:true" onclick="xiuGai();">修改</a>
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>


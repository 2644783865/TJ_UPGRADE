<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_SALARYBASEDATA.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_SALARYBASEDATA" Title="无标题页" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">


    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312"
        language="javascript"></script>

    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        
        function btnclose_click() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'none';
        }
    </script>
    <script language="javascript" type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 2,
                fixedCols: 6,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    
                    }
            });
        }

        $(function() {
            sTable();
        });
    </script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;<strong>部门：</strong>
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>姓名：</strong><asp:TextBox ID="txtName"
                            runat="server" Width="90px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>工号：</strong><asp:TextBox ID="txtgh" runat="server"
                            Width="90px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>岗位序列：</strong><asp:TextBox ID="txtgwxl"
                            runat="server" Width="90px"></asp:TextBox>
                        <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('OM_SALARYBASEDATADETAIL_ADDDELETE.aspx?FLAG=add','','dialogWidth=  1200px;dialogHeight=500px');" runat="server">
                            <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除"  OnClick="btnDelete_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;

                        <asp:Button runat="server" ID="btn_plexport" Text="导出" OnClick="btn_plexport_OnClick" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnplupdate" Text="数据批量更新" OnClientClick="viewCondition()" />
                        <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnplupdate"
                            PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                            Y="80" X="700">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:RadioButton ID="radio_all" runat="server" Text="全部" GroupName="zhistate" OnCheckedChanged="radiozhistate_CheckedChanged"
                                            AutoPostBack="True" Checked="true" />
                        <asp:RadioButton ID="radio_zaizhi" runat="server" Text="在职" GroupName="zhistate" OnCheckedChanged="radiozhistate_CheckedChanged"
                                            AutoPostBack="True" />
                        <asp:RadioButton ID="radio_lizhi" runat="server" Text="离职" GroupName="zhistate" OnCheckedChanged="radiozhistate_CheckedChanged"
                                            AutoPostBack="True" />
                        <asp:RadioButton ID="radio_other" runat="server" Text="其他" GroupName="zhistate" OnCheckedChanged="radiozhistate_CheckedChanged"
                                            AutoPostBack="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/OM_Data/OM_SALARYBASEDATAJLCK.aspx?" Target="_blank" Font-Underline="false"><asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />查询修改记录</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            
            <asp:Panel ID="PanelCondition" runat="server" Width="300px" Style="display: none">
            <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnClose" runat="server" OnClientClick="btnclose_click" Text="关闭" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="iptbingjia" runat="server" />
                        <input type="button" id="btnbingjia" value="更新病假工资基数" onserverclick="btnbingjia_click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="iptjiaban" runat="server" />
                        <input type="button" id="btnjiaban" value="更新加班工资基数" onserverclick="btnjiaban_click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="iptnianjia" runat="server" />
                        <input type="button" id="btnnianjia" value="更新年假工资基数" onserverclick="btnnianjia_click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="btnykgw" value="更新应扣岗位基数(同步固定工资)" onserverclick="btnykgw_click" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <input type="button" id="btnupdateperinfo" value="更新人员信息" onserverclick="btnupdateperinfo_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
    </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div style="height: 475px; overflow: auto; width: 100%">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="rptbasedata" runat="server">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center" rowspan="2">
                                            序号
                                        </td>
                                        <td align="center" rowspan="2">
                                            工号
                                        </td>
                                        <td align="center" rowspan="2">
                                            姓名
                                        </td>
                                        <td align="center" rowspan="2">
                                            部门
                                        </td>
                                        <td align="center" rowspan="2">
                                            岗位序列
                                        </td>
                                        <td align="center" rowspan="2">
                                            状态
                                        </td>
                                        <td align="center" colspan="7">
                                            病假工资基数
                                        </td>
                                        <td align="center" colspan="7">
                                            加班工资基数
                                        </td>
                                        <td align="center" colspan="7">
                                            年假工资基数
                                        </td>
                                        <td align="center" colspan="7">
                                            应扣岗位基数
                                        </td>
                                    </tr>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center">
                                            当前基数
                                        </td>
                                        <td align="center">
                                            最近操作人
                                        </td>
                                        <td align="center">
                                            最近操作时间
                                        </td>
                                        <td>
                                            旧基数
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td>
                                            操作记录
                                        </td>
                                        
                                        <td align="center">
                                            当前基数
                                        </td>
                                        <td align="center">
                                            最近操作人
                                        </td>
                                        <td align="center">
                                            最近操作时间
                                        </td>
                                        <td>
                                            旧基数
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td>
                                            操作记录
                                        </td>
                                        
                                        <td align="center">
                                            当前基数
                                        </td>
                                        <td align="center">
                                            最近操作人
                                        </td>
                                        <td align="center">
                                            最近操作时间
                                        </td>
                                        <td>
                                            旧基数
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td>
                                            操作记录
                                        </td>
                                        
                                        <td align="center">
                                            当前基数
                                        </td>
                                        <td align="center">
                                            最近操作人
                                        </td>
                                        <td align="center">
                                            最近操作时间
                                        </td>
                                        <td>
                                            旧基数
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td>
                                            操作记录
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                            <asp:Label ID="ST_ID" runat="server" Text='<%#Eval("ST_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_gh" runat="server" Text='<%#Eval("PERSON_GH")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lb_bm" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbgwxl" runat="server" Text='<%#Eval("ST_SEQUEN")%>'></asp:Label>
                                        </td>
                                        
                                        <td>
                                            <asp:Label ID="lbstate" runat="server" Text='<%#Eval("ST_PD").ToString()=="0"?"在职":Eval("ST_PD").ToString()=="1"?"离职":"其他"%>'></asp:Label>
                                        </td>
                                        
                                        <td>
                                            <asp:Label ID="BINGJIA_BASEDATANEW" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("BINGJIA_BASEDATANEW")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BINGJIA_CZRNAME" runat="server" Text='<%#Eval("BINGJIA_CZRNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BINGJIA_CZTIME" runat="server" Text='<%#Eval("BINGJIA_CZTIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="BINGJIA_BASEDATAOLD" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("BINGJIA_BASEDATAOLD")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="BINGJIA_NOTE" runat="server" Width="100px" Text='<%#Eval("BINGJIA_NOTE")%>' BorderStyle="None" ToolTip='<%#Eval("BINGJIA_NOTE")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="editbingjia" NavigateUrl='<%# editDq(Eval("ST_ID").ToString(),"BINGJIA") %>'
                                                runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lookbingjia" NavigateUrl='<%# CKDq(Eval("ST_ID").ToString(),"BINGJIA") %>'
                                                runat="server">
                                                <strong>查看</strong></asp:HyperLink>
                                        </td>
                                        
                                        
                                         <td>
                                            <asp:Label ID="JIABAN_BASEDATANEW" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("JIABAN_BASEDATANEW")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="JIABAN_CZRNAME" runat="server" Text='<%#Eval("JIABAN_CZRNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="JIABAN_CZTIME" runat="server" Text='<%#Eval("JIABAN_CZTIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="JIABAN_BASEDATAOLD" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("JIABAN_BASEDATAOLD")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="JIABAN_NOTE" runat="server" Width="150px" Text='<%#Eval("JIABAN_NOTE")%>' BorderStyle="None" ToolTip='<%#Eval("JIABAN_NOTE")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="editjiaban" NavigateUrl='<%# editDq(Eval("ST_ID").ToString(),"JIABAN") %>'
                                                runat="server">
                                                <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lookjiaban" NavigateUrl='<%# CKDq(Eval("ST_ID").ToString(),"JIABAN") %>'
                                                runat="server">
                                                <strong>查看</strong></asp:HyperLink>
                                        </td>
                                        
                                        
                                         <td>
                                            <asp:Label ID="NIANJIA_BASEDATANEW" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("NIANJIA_BASEDATANEW")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="NIANJIA_CZRNAME" runat="server" Text='<%#Eval("NIANJIA_CZRNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="NIANJIA_CZTIME" runat="server" Text='<%#Eval("NIANJIA_CZTIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="NIANJIA_BASEDATAOLD" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("NIANJIA_BASEDATAOLD")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="NIANJIA_NOTE" runat="server" Width="150px" Text='<%#Eval("NIANJIA_NOTE")%>' BorderStyle="None"
                                                Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("NIANJIA_NOTE")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="editnianjia" NavigateUrl='<%# editDq(Eval("ST_ID").ToString(),"NIANJIA") %>'
                                                runat="server">
                                                <asp:Image ID="Image3" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="looknianjia" NavigateUrl='<%# CKDq(Eval("ST_ID").ToString(),"NIANJIA") %>'
                                                runat="server">
                                                <strong>查看</strong></asp:HyperLink>
                                        </td>
                                        
                                        
                                         <td>
                                            <asp:Label ID="YKGW_BASEDATANEW" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("YKGW_BASEDATANEW")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="YKGW_CZRNAME" runat="server" Text='<%#Eval("YKGW_CZRNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="YKGW_CZTIME" runat="server" Text='<%#Eval("YKGW_CZTIME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="YKGW_BASEDATAOLD" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("YKGW_BASEDATAOLD")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YKGW_NOTE" runat="server" Width="150px" Text='<%#Eval("YKGW_NOTE")%>' BorderStyle="None" ToolTip='<%#Eval("YKGW_NOTE")%>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="editykgw" NavigateUrl='<%# editDq(Eval("ST_ID").ToString(),"YKGW") %>'
                                                runat="server">
                                                <asp:Image ID="Image4" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lookykgw" NavigateUrl='<%# CKDq(Eval("ST_ID").ToString(),"YKGW") %>'
                                                runat="server">
                                                <strong>查看</strong></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                            没有记录!<br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>

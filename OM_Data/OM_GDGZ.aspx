<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_GDGZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GDGZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">

    <script type="text/javascript">
    
    function GouXuan(){
var ids = $("#table1").find("input[type='checkbox']:checked")
.map(function(){
        return $(this).val();
    }).get().join(",");
    window.location.href = "OM_GDGZSCSP_detail.aspx?action=add&id="+ids;
    }
    </script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../FM_Data/FM_JS/SelectCondition.js" type="text/javascript"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div class="box-wrapper">
        <div class="box-outer">
            <table>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;<strong>部门：</strong>&nbsp;
                        <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                            OnSelectedIndexChanged="dplbm_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>姓名：</strong><asp:TextBox ID="txtName"
                            runat="server" Width="90px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>工号：</strong><asp:TextBox ID="txtgh" runat="server"
                            Width="90px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>岗位序列：</strong><asp:TextBox ID="txtgwxl"
                            runat="server" Width="90px"></asp:TextBox>
                        <asp:Button ID="btncx" runat="server" Text="查询" OnClick="btncx_click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('OM_GDGZAdd.aspx?FLAG=add','','dialogWidth=  1200px;dialogHeight=500px');" runat="server">
                            <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            添加</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="删除"  OnClick="btnDelete_OnClick" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="120px" ToolTip="导 入" Visible="false" />
                        <asp:Button runat="server" ID="btnDaoRu" Text="导 入" OnClick="btnDaoRu_OnClick" Visible="false" />&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btn_plexport" Text="导出" OnClick="btn_plexport_OnClick" />
                        &nbsp;&nbsp;&nbsp;
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
                        <asp:HyperLink ID="hplfjcb" runat="server" NavigateUrl="~/OM_Data/OM_GDGZJLCK.aspx?" Target="_blank" Font-Underline="false"><asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" Width="20px" />查询修改记录</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
                    <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                        <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                            border="1" width="100%">
                            <asp:Repeater ID="rptGDGZ" runat="server" OnItemDataBound="rptGDGZ_itemdatabind">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td align="center">
                                            序号
                                        </td>
                                        <td align="center">
                                            工号
                                        </td>
                                        <td align="center">
                                            姓名
                                        </td>
                                        <td align="center">
                                            部门
                                        </td>
                                        <td align="center">
                                            岗位序列
                                        </td>
                                        <td>
                                            状态
                                        </td>
                                        <td align="center">
                                            固定工资
                                        </td>
                                        <td align="center">
                                            最近维护人
                                        </td>
                                        <td align="center">
                                            最近维护时间
                                        </td>
                                        <td>
                                            调整前固定工资
                                        </td>
                                        <td>
                                            调整额度
                                        </td>
                                        <td align="center">
                                            备注
                                        </td>
                                        <td align="center">
                                            修改
                                        </td>
                                        <td>
                                            固定工资记录
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                        ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                        <td align="center">
                                            <asp:CheckBox ID="cbxSelect" CssClass="checkBoxCss" runat="server" Onclick="checkme(this)" />
                                            <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                            <asp:Label ID="lbstid" runat="server" Text='<%#Eval("ST_ID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lb_gh" runat="server" Text='<%#Eval("Person_GH")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lb_name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lb_bm" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbgwxl" runat="server" Text='<%#Eval("ST_SEQUEN")%>'></asp:Label>
                                        </td>
                                        
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbstate" runat="server" Text='<%#Eval("ST_PD").ToString()=="0"?"在职":Eval("ST_PD").ToString()=="1"?"离职":"其他"%>'></asp:Label>
                                        </td>
                                        
                                        <td runat="server" align="center">
                                            <asp:Label ID="lb_gdgz" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("GDGZ")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbxgr" runat="server" Text='<%#Eval("XGRST_NAME")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:Label ID="lbxgtime" runat="server" Text='<%#Eval("XGTIME")%>'></asp:Label>
                                        </td>
                                        <td id="Td1" runat="server" align="center">
                                            <asp:Label ID="lblastgdgz" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("lastgdgz")%>'></asp:Label>
                                        </td>
                                        <td id="Td2" runat="server" align="center">
                                            <asp:Label ID="lbtzedu" runat="server" BorderStyle="None" BackColor="Transparent"
                                                Width="50px" Text='<%#Eval("tzedu").ToString()=="0"?"":Eval("tzedu")%>'></asp:Label>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:TextBox ID="ptcode" runat="server" Width="150px" Text='<%#Eval("NOTE")%>' BorderStyle="None"
                                                Style="background-color: Transparent; text-align: center" ToolTip='<%#Eval("NOTE")%>'></asp:TextBox>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("ST_ID").ToString()) %>'
                                                runat="server">
                                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                                    runat="server" />修改</asp:HyperLink>
                                        </td>
                                        <td runat="server" align="center">
                                            <asp:HyperLink ID="HyperLinkCK" NavigateUrl='<%# CKDq(Eval("ST_ID").ToString()) %>'
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

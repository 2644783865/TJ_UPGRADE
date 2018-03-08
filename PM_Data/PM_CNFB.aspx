<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PM_CNFB.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_CNFB" Title="无标题页" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<strong>厂内分包信息</strong>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
 <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/FM_Cost.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
$(function(){
$("#Checkbox2").click(function(){
if($("#Checkbox2").attr("checked")){
 $("#table1 input[type=checkbox]").attr("checked","true");
}
else{
 $("#table1 input[type=checkbox]").removeAttr("checked");
}
});})//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="False"></asp:Label>
        <div class="box-outer">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                全选/取消<input id="Checkbox2" type="checkbox" />
                            </td>
                            <td style="width: 23%;">
                                <strong>时间：</strong>
                                <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;年&nbsp;
                                <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                                &nbsp;月&nbsp;
                            </td>
                            <td align="center">
                                <strong>铆焊班组：</strong>
                                <asp:DropDownList ID="DropDownListZB" runat="server" Width="100px">
                                                        
                               </asp:DropDownList>
                            </td>
                            <td align="center" style="width:26%">
                                <strong>任务号:</strong><asp:TextBox ID="txtrwh" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_OnClick" />
                            </td>
                            <td style="width:28%" align="left">
                                <asp:Button ID="Button1" runat="server" Text="导出数据" OnClick="btn_export_Click" />
                            </td>
                          </tr>  
                    </table>
                </div>
            </div>
        </div>
                <div class="box-outer">
                    <table width="98%">
                       <tr>
                            <td style="width:20%">
                                <asp:FileUpload runat="server" ID="FileUpload1" Width="200px" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btn" runat="server" Text="导入数据" OnClick="btn_import_Click" />
                            </td>
                            <td>
                                <strong>年份(格式：YYYY)</strong>
                                <asp:TextBox ID="tbyear" runat="server"></asp:TextBox>
                                <strong>月份(格式：MM)</strong>
                                <asp:TextBox ID="tbmonth" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 200px"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('PM_CNFB_ADD.aspx?','','dialogWidth=650px;dialogHeight=400px');" runat="server">
                          <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                             添加</asp:HyperLink>&nbsp;&nbsp;
                            </td>
                            <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="删除" OnClientClick="javascript:return confirm('确定要删除吗？');" onclick="btnSC_Click"  />
                            </td>
                          </tr>
                    </table>
                  </div>
                <div  style="overflow: scroll;height: 400px;">
                <table id="table1" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%" >
                    <asp:Repeater ID="rptProNumCost" runat="server" OnItemDataBound="rptProNumCost_OnItemDataBound">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle headcolor">
                                <th>
                                    序号
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    合同号
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    图号
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <%--<th>
                                    数量
                                </th>--%>
                                <th>
                                    本月理论结算金额（元）
                                </th>
                                <th>
                                    本月实际结算金额（元）
                                </th>
                                <th>
                                    年份
                                </th>
                                <th>
                                    月份
                                </th>
                                <th>
                                    班组
                                </th>
                                <th>
                                    修改
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" align="center" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                <asp:Label ID="lblID" runat="server" visible="false" Text='<%#Eval("ID")%>'></asp:Label>
                                <td>
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;<asp:CheckBox ID="chkDel"
                                        runat="server" />    
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbprojname" runat="server" Text='<%#Eval("CNFB_PROJNAME")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbprojid" runat="server" Text='<%#Eval("CNFB_HTID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbrwh" runat="server" Text='<%#Eval("CNFB_TSAID")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbth" runat="server" Text='<%#Eval("CNFB_TH")%>'></asp:Label>
                                </td>
                                <td style="width:300px;white-space:normal" align="center">
                                    <asp:Label ID="lbsbname" runat="server" Text='<%#Eval("CNFB_SBNAME")%>'></asp:Label>
                                </td>
                                <%--<td align="center">
                                    <asp:Label ID="lbsl" runat="server" Text='<%#Eval("CNFB_NUM")%>'></asp:Label>
                                </td>--%>
                                <td align="center">
                                    <asp:Label ID="lbbymymoney" runat="server" align="center" Text='<%#Eval("CNFB_BYMYMONEY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbbyrealmoney" runat="server" align="center" Text='<%#Eval("CNFB_BYREALMONEY")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbyear" runat="server" align="center" Text='<%#Eval("CNFB_YEAR")%>'></asp:Label>
                                </td>
                                
                                
                                <td align="center">
                                    <asp:Label ID="lbmonth" runat="server" align="center" Text='<%#Eval("CNFB_MONTH")%>'></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbtype" runat="server" align="center" Text='<%#Eval("CNFB_TYPE")%>'></asp:Label>
                                </td>
                                <td><asp:HyperLink ID="HyperLinkXG" NavigateUrl='<%# editDq(Eval("ID").ToString()) %>'  runat="server" >
                            <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                                修改</asp:HyperLink>
                            </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="6" align="right">
                                    合计：
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbmyjehj" runat="server"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lbsjjehj" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" HorizontalAlign="Center">
                    没有记录!</asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>

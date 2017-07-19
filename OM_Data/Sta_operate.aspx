<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sta_operate.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.Sta_operate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员信息</title>
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <div class="box_right" style="height: 20px; padding-top: 5px; width: 800px">
            &nbsp;&nbsp;&nbsp;填表日期：<asp:Label runat="server" ID="ST_FILLDATE"></asp:Label>
            &nbsp;&nbsp;&nbsp;维护人：<asp:Label runat="server" ID="MANCLERK">
            </asp:Label>
        </div>
        <div class="box-wrapper" style="width: 800px">
            <div class="box-outer">
                <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
                    text-align: left; width: 750px;" class="toptable grid" border="1">
                    <tr>
                        <td style="width: 80px; height: 25px">
                            姓名：
                        </td>
                        <td class="backgrd colstle">
                            <asp:Label runat="server" ID="ST_NAME"></asp:Label>
                        </td>
                        <td class="rowstle">
                            性别：
                        </td>
                        <td class="backgrd" style="width: 120px">
                            <asp:Label runat="server" ID="ST_GENDER"></asp:Label>
                        </td>
                        <td class="rowstle">
                            年龄：
                        </td>
                        <td class="backgrd" style="width: 120px">
                            <asp:Label runat="server" ID="ST_AGE"></asp:Label>
                        </td>
                        <td rowspan="4" style="width: 100px">
                            <asp:Image ID="showImage" runat="server" Width="100px" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="colstle">
                            序列：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_SEQUEN"></asp:Label>
                        </td>
                        <td>
                            部门：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="DEP_NAME"></asp:Label>
                        </td>
                        <td>
                            职位：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="DEP_POSITION"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="colstle">
                            民族：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_PEOPLE"></asp:Label>
                        </td>
                        <td>
                            政治面貌：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_POLITICAL"></asp:Label>
                        </td>
                        <td>
                            出生日期：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_BIRTHDAY"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="colstle">
                            户口：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_REGIST"></asp:Label>
                        </td>
                        <td>
                            联系电话：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_TELE"></asp:Label>
                        </td>
                        <td>
                            身份证号：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_IDCARD"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="colstle">
                            学历：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_XUELI"></asp:Label>
                        </td>
                       
                        <td class="colstle">
                            学位：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_XUEWEI"></asp:Label>
                        </td>
                         <td>
                            专业：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_ZHUANYE"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            岗位名称：
                        </td>
                        <td class="backgrd">
                            <asp:Label runat="server" ID="ST_ZHICH"></asp:Label>
                        </td>
                        <td class="colstle">
                            毕业院校：
                        </td>
                        <td colspan="2" class="backgrd">
                            <asp:Label runat="server" ID="ST_BIYE"></asp:Label>
                        </td>
                        <td  class="backgrd">
                         Email:
                        </td>
                        <td >  
                        <asp:Label runat="server" ID="EMAIL"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="width: 780px">
                    工作履历表：&nbsp;&nbsp;&nbsp;&nbsp;
                    <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Det_Repeater" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td width="50px">
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>起&nbsp;/&nbsp;止年月</strong>
                                    </td>
                                    <td>
                                        <strong>工&nbsp;作&nbsp;单&nbsp;位</strong>
                                    </td>
                                    <td>
                                        <strong>职位或者岗位</strong>
                                    </td>
                                    <td>
                                        <strong>离职原因</strong>
                                    </td>
                                    <td>
                                        <strong>月薪</strong>
                                    </td>
                                    <td>
                                        <strong>证明人</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                    </td>
                                    <td>
                                        <%# Eval("ST_GZSTART")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_GZDW")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_POSITION")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_REAOUT")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_SALARY")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_INDENTITY")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                        没有记录!
                    </asp:Panel>
                </div>
                <div style="width: 780px">
                    教育及其培训记录经历：&nbsp;&nbsp;&nbsp;&nbsp;
                    <table id="Table1" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Det_Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td width="50px">
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>起&nbsp;/&nbsp;止年月</strong>
                                    </td>
                                    <td>
                                        <strong>学校名称及培训机构</strong>
                                    </td>
                                    <td>
                                        <strong>专业</strong>
                                    </td>
                                    <td>
                                        <strong>外语语种及程度</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                    </td>
                                    <td>
                                        <%# Eval("ST_JYTIME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_SCHOOL")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_ZHUAN")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_ENGLISH")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center">
                        没有记录!
                    </asp:Panel>
                </div>
                <div style="width: 780px">
                    家庭成员：&nbsp;&nbsp;&nbsp;&nbsp;
                    <table id="Table2" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Det_Repeater2" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle headcolor">
                                    <td width="50px">
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>姓名</strong>
                                    </td>
                                    <td>
                                        <strong>年龄</strong>
                                    </td>
                                    <td>
                                        <strong>与本人关系</strong>
                                    </td>
                                    <td>
                                        <strong>工作单位及职务</strong>
                                    </td>
                                     <td>
                                        <strong>联系电话</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                    </td>
                                    <td>
                                        <%# Eval("ST_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_AGE")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_RELATION")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_WORK")%>
                                    </td>
                                    <td>
                                        <%# Eval("ST_TELE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center">
                        没有记录!
                    </asp:Panel>
                </div>
            </div>
        </div>
    </asp:Panel>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_KaoheRecord.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_KaoheRecord" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    考核清单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
        $(function() {
            $("#Checkbox2").click(function() {
                if ($("#Checkbox2").attr("checked")) {
                    $("#tab input[type=checkbox]").attr("checked", "true");
                }
                else {
                    $("#tab input[type=checkbox]").removeAttr("checked");
                }
            });
        })//jquery的写法，先声明一个函数，然后捕捉触发事件的对象，触发该对象时执行的事件（函数），遍历某些特定的控件，判断对象是否触发，执行事件；
    </script>

    <script type="text/javascript">
        $(function() {
            $("#tab input[name*=txtScoreHP],#tab input[name*=txtlScoreLD]").keyup(function() {
                var score = new Object();
                var $tr = $(this).parent().parent();
                score.hp = $tr.find("input[name*=txtScoreHP]").val();
                score.ld = $tr.find("input[name*=txtlScoreLD]").val();
                score.bl = $tr.find("span[name*=lblBl]").html();
                // score.zongf = $tr.find("span[name*=lblScoreZong]").html();
                score.zongf = score.hp * score.bl.split(':')[0] + score.ld * score.bl.split(':')[1];
                $tr.find("span[name*=lblScoreZong]").html((score.zongf / 100).toFixed(2));
                $tr.find("input[name*=hidScoreZong]").val((score.zongf / 100).toFixed(2));
                //    $tr.find("input[name*=txtScoreZong]").val(score.zongf / 100);
            });
        });
    
    </script>

    <div class="box-wrapper">
        <div class="box-outer">
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width:330px" >
                            <strong>时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMoth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
                        </td>
             
                        <td>部门：
                            <asp:DropDownList ID="ddl_Depart" runat="server" Width="100px" AutoPostBack="true"
                                OnSelectedIndexChanged="dplMoth_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width:260px">
                            <strong>按姓名查询：</strong><asp:TextBox ID="txtname" ForeColor="Gray" runat="server"
                                onfocus="DefaultTextOnFocus(this);" onblur="DefaultTextOnBlur(this);" Width="80px"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click"></asp:Button>
                            &nbsp;
                          
                          
                        </td>
                       <td style="width:300px">
                       
                      
                            <asp:FileUpload ID="FileUpload1" Width="130px" runat="server" ToolTip="导 入" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnimport" Text="导入" OnClientClick="viewCondition()" />
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnimport"
                                PopupControlID="PanelCondition" Drag="True" Enabled="True" DynamicServicePath=""
                                Y="80" X="900">
                            </asp:ModalPopupExtender>
                            &nbsp;&nbsp;&nbsp;
                        
                       </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                     
                     <td style="width:100%" colspan="3">
                      <label><font color="red">使用说明：1.选择部门 2.输入互评、领导评分比例 3.点击“同步考核数据”按钮 4.修改成绩后，点击“保存”按钮保存</font></label>
                          &nbsp; 比例：互评<asp:TextBox ID="txtHP" runat="server" Width="30px"></asp:TextBox>
                            领导评分<asp:TextBox ID="txtLD" runat="server" Width="30px"></asp:TextBox>
                            <asp:Button ID="btnCreat" runat="server" Text="同步该月考核记录" OnClick="btnCreat_Click"
                                OnClientClick="return confirm('您确定要生成当月数据吗？如果确定将清楚当月数据并重新生成！')" /> &nbsp; &nbsp;   <asp:Button ID="btnSave" runat="server" Text="保存考核成绩" OnClick="btnSave_Click" />
                      
                        </td>
                      
                    
                    </tr>
                    <tr>
                        <td>
                            全选/取消<input id="Checkbox2" type="checkbox" />
                            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" />
                            <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server" NavigateUrl="~/OM_Data/OM_KaoHe_JXGZ.aspx" Visible="false">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" Visible="false" />计算绩效工资</asp:HyperLink>
                        </td>
                       
                       
                       
                      
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition" runat="server" Width="300px" Style="display: none">
                    <table width="90%" style="background-color: #CCCCFF; border: solid 1px black;">
                        <tr>
                            <td colspan="8" align="center">
                                <asp:Button ID="QueryButton" runat="server" OnClick="btnimport_Click" Text="确认" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnClose" runat="server" OnClick="btnqx_import_Click" Text="取消" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>年月：</strong><asp:TextBox ID="tb_Time" Width="120px" runat="server"></asp:TextBox>
                                <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                                    ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_Time">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div id="div_statistcs" style="width: 100%; height: auto; overflow: scroll; display: block;">
                <table id="tab" align="center" cellpadding="2" cellspacing="1" class="toptable grid nowrap"
                    border="1" width="100%">
                    <asp:Repeater ID="rptProNumCost" runat="server">
                        <HeaderTemplate>
                            <tr align="center" style="background-color: #B9D3EE;">
                                <td>
                                    序号
                                </td>
                                <td>
                                    年
                                </td>
                                <td>
                                    月
                                </td>
                                <td>
                                    部门
                                </td>
                                <td>
                                    岗位
                                </td>
                                <td>
                                    工号
                                </td>
                                <td>
                                    岗位序列
                                </td>
                                <td>
                                    姓名
                                </td>
                                <td>
                                    互评分数
                                </td>
                                <td>
                                    领导评分
                                </td>
                                <td>
                                    总分数
                                </td>
                                <td>
                                    比例
                                </td>
                                <td>
                                    备注
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);">
                                <td>
                                    <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" runat="server" Checked="false"
                                        Onclick="checkme(this)" />
                                    <%# Convert.ToInt32(Container.ItemIndex + 1)%>&nbsp;
                                </td>
                                <td id="Td1" runat="server" align="center">
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Kh_Year")%>'></asp:Label>
                                </td>
                                <td id="Td2" runat="server" align="center">
                                    <asp:Label ID="lblMonth" runat="server" Text='<%#Eval("Kh_Month")%>'></asp:Label>
                                </td>
                                <td id="Td3" runat="server" align="center">
                                    <asp:Label ID="lblPart" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td4" runat="server" align="center">
                                    <asp:Label ID="lblPosition" runat="server" Text='<%#Eval("POSITION")%>'></asp:Label>
                                </td>
                                <td id="Td12" runat="server" align="center">
                                    <asp:Label ID="lblSquence" runat="server" Text='<%#Eval("ST_SEQUEN")%>'></asp:Label>
                                </td>
                                <td id="Td5" runat="server" align="center">
                                    <asp:Label ID="lblWorkNum" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                </td>
                                <td id="Td6" runat="server" align="center">
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                </td>
                                <td id="Td7" runat="server" align="center">
                                    <asp:TextBox ID="txtScoreHP" runat="server" align="center" Width="70px" BorderStyle="None"
                                        BackColor="Transparent" Text='<%#Eval("Kh_ScoreHP")%>' name="txtScoreHP"></asp:TextBox>
                                </td>
                                <td id="Td8" runat="server" align="center">
                                    <asp:TextBox ID="txtlScoreLD" runat="server" align="center" Width="70px" BorderStyle="None"
                                        BackColor="Transparent" Text='<%#Eval("Kh_ScoreLD")%>' name="txtlScoreLD" onfocus="javascript:setToolTipGet(this);"
                                        onblur="javascript:setToolTipLost(this)"></asp:TextBox>
                                </td>
                                <td id="Td9" align="center" runat="server">
                                    <asp:Label ID="lblScoreZong" runat="server" Text='<%#Eval("Kh_ScoreTotal")%>' name="lblScoreZong"></asp:Label>
                                    <input type="hidden" runat="server" id="hidScoreZong" name="hidScoreZong" value='<%#Eval("Kh_ScoreTotal")%>' />
                                </td>
                                <td id="Td10" runat="server" align="center">
                                    <asp:Label ID="lblBl" runat="server" Text='<%#Eval("Kh_BL")%>' name="lblBl"></asp:Label>
                                </td>
                                <td id="Td11" runat="server" align="center">
                                    <asp:TextBox ID="txtNote" runat="server" align="center" Width="200px" BorderStyle="None"
                                        BackColor="Transparent" Text='<%#Eval("Kh_BeiZhu")%>' onfocus="javascript:setToolTipGet(this);"
                                        onblur="javascript:setToolTipLost(this)"></asp:TextBox>
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
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblTip" runat="server" Text="提示:" Visible="false" Font-Bold="true"></asp:Label>
                <asp:Image ID="imgloading" runat="server" Visible="false" />
                <asp:Label ID="lblTip2" runat="server" Visible="false" Text="数据处理中，请稍后..."></asp:Label>
                <asp:Label ID="LabelDate" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

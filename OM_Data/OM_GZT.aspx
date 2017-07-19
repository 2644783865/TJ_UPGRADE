<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="OM_GZT.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_GZHZB" Title="工资条" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工资条
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <script language="javascript" type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 9,
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

    <script language="javascript" type="text/javascript">
    function gzqd(myButton) {
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }
            if (myButton.getAttribute('type') == 'button') {
                myButton.disabled = true;
                myButton.value = "加载中...";              
            }
           return true;
        }
    </script>
    <asp:ToolkitScriptManager runat="server" AsyncPostBackTimeout="10" ID="ToolkitScriptManager1">
    </asp:ToolkitScriptManager>
        <div class="box-inner">
            <table style="width: 100%">
                <tr>
                    <td style="width: 23%;" align="left">
                        <strong>时间：</strong>
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;年&nbsp;
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;月&nbsp;
                    </td>
                    <td align="right">
                        勾选隐藏列：
                        <asp:CheckBox ID="cbxBumen" runat="server" AutoPostBack="true" OnCheckedChanged="cbxBumen_CheckedChanged" />
                        部门 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="cbxKaoqin" runat="server" AutoPostBack="true" OnCheckedChanged="cbxKaoqin_CheckedChanged" />
                        考勤 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="cbxWXYJ" runat="server" AutoPostBack="true" OnCheckedChanged="cbxWXYJ_CheckedChanged" />
                        五险一金&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
             <div class="box-wrapper">
                <div class="box-outer">
                    <div style="height: 475px; overflow: auto; width: 100%">
                        <div class="cpbox xscroll">
                        <table id="tab" class="nowrap cptable fullwidth" align="center">
                            <asp:Repeater ID="rptGZQD" runat="server" OnItemDataBound="rptGZQD_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" style="background-color: #B9D3EE;">
                                        <td>
                                            序号
                                        </td>
                                        <td>
                                            编号
                                        </td>
                                        <td>
                                            年月份
                                        </td>
                                        <td>
                                            工号
                                        </td>
                                        <td>
                                            姓名
                                        </td>
                                        <td>
                                            合同主体
                                        </td>
                                        <td>
                                            区分标识
                                        </td>
                                        <td runat="server" id="tdBumen">
                                            部门
                                        </td>
                                        <td runat="server" id="tdBanzu">
                                            班组
                                        </td>
                                        <td>
                                            岗位
                                        </td>
                                        <td runat="server" id="tdYCQHJ">
                                            出勤
                                        </td>
                                        <td runat="server" id="tdJRwork">
                                            节日工作
                                        </td>
                                        <td runat="server" id="tdZhouwork">
                                            周休息工作
                                        </td>
                                        <td runat="server" id="tdRiwork">
                                            日延时工作
                                        </td>
                                        <td runat="server" id="tdBingjia">
                                            病假
                                        </td>
                                        <td runat="server" id="tdShijia">
                                            事假
                                        </td>
                                        <td runat="server" id="tdNianjia">
                                            年假
                                        </td>
                                        <td>
                                            基础工资
                                        </td>
                                        <td>
                                            工龄工资
                                        </td>
                                        <td>
                                            固定工资
                                        </td>
                                        <td>
                                            绩效工资
                                        </td>
                                        <td>
                                            奖励
                                        </td>
                                        <td>
                                            病假工资
                                        </td>
                                        <td>
                                            加班工资
                                        </td>
                                        <td>
                                            加班补发
                                        </td>
                                        <td>
                                            中夜班费
                                        </td>
                                        <td>
                                            中夜班补发
                                        </td>
                                        <td>
                                            年假工资
                                        </td>
                                        <td>
                                            应扣岗位
                                        </td>
                                        <td>
                                            调整/补发
                                        </td>
                                        <td>
                                            调整/补扣
                                        </td>
                                        <td>
                                            交通补贴
                                        </td>
                                        <td>
                                            防暑降温费
                                        </td>
                                        <td>
                                            采暖补贴
                                        </td>
                                        <td>
                                            其他
                                        </td>
                                        <td>
                                            应付合计
                                        </td>
                                        <td runat="server" id="tdYangLBX">
                                            养老保险
                                        </td>
                                        <td runat="server" id="tdSYBX">
                                            失业保险
                                        </td>
                                        <td runat="server" id="tdYiLBX">
                                            医疗保险
                                        </td>
                                        <td runat="server" id="tdDEJiuZhu">
                                            大额救助
                                        </td>
                                        <td runat="server" id="tdBuBX">
                                            补保险
                                        </td>
                                        <td runat="server" id="tdGJJ">
                                            公积金
                                        </td>
                                        <td runat="server" id="tdBGJJ">
                                            补公积金
                                        </td>
                                        <td>
                                            房租/水电费
                                        </td>
                                        <td>
                                            个税
                                        </td>
                                        <td>
                                            代扣小计
                                        </td>
                                        <td>
                                            实发金额
                                        </td>
                                        <td>
                                            扣税基数
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr id="row" class="baseGadget" onmouseover="this.className='highlight'" onclick="javascript:change(this);"
                                ondblclick="javascript:changeback(this);" onmouseout="this.className='baseGadget'">
                                        <td>
                                            <asp:Label runat="server" ID="lbQD_ID" Visible="false" Text='<%#Eval("QD_ID")%>'></asp:Label>
                                            <asp:CheckBox ID="cbxNumber" CssClass="checkBoxCss" runat="server" />
                                            <%# Container.ItemIndex + 1 + (Convert.ToDouble(UCPaging1.CurrentPage) - 1) * (Convert.ToDouble(UCPaging1.PageSize))%>&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lbQD_SHBH" runat="server" Text='<%#Eval("QD_SHBH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_YEARMONTH" runat="server" Text='<%#Eval("QD_YEARMONTH")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_Worknumber" runat="server" Text='<%#Eval("ST_WORKNO")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_Name" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_HTZT" runat="server" Text='<%#Eval("QD_HTZT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_QuFen" runat="server" Text='<%#Eval("QD_QFBS")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BuMen" runat="server" align="center">
                                            <asp:Label ID="lbQD_BuMen" runat="server" Text='<%#Eval("DEP_NAME")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BanZu" runat="server" align="center">
                                            <asp:Label ID="lbQD_BanZu" runat="server" Text='<%#Eval("ST_DEPID1")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GangWei" runat="server" Text='<%#Eval("DEP_NAME_POSITION")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td id="tdKQ_CHUQIN" runat="server" align="center">
                                            <asp:Label ID="lbKQ_CHUQIN" runat="server" Text='<%#Eval("KQ_CHUQIN")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_JRJIAB" runat="server" align="center">
                                            <asp:Label ID="lbKQ_JRJIAB" runat="server" Text='<%#Eval("KQ_JRJIAB")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_ZMJBAN" runat="server" align="center">
                                            <asp:Label ID="lbKQ_ZMJBAN" runat="server" Text='<%#Eval("KQ_ZMJBAN")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_YSGZ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_YSGZ" runat="server" Text='<%#Eval("KQ_YSGZ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_BINGJ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_BINGJ" runat="server" Text='<%#Eval("KQ_BINGJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_SHIJ" runat="server" align="center">
                                            <asp:Label ID="lbKQ_SHIJ" runat="server" Text='<%#Eval("KQ_SHIJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdKQ_NIANX" runat="server" align="center">
                                            <asp:Label ID="lbKQ_NIANX" runat="server" Text='<%#Eval("KQ_NIANX")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        <td align="center">
                                            <asp:Label ID="lbQD_JCGZ" runat="server" Text='<%#Eval("QD_JCGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GZGL" runat="server" Text='<%#Eval("QD_GZGL")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GDGZ" runat="server" Text='<%#Eval("QD_GDGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JXGZ" runat="server" Text='<%#Eval("QD_JXGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JiangLi" runat="server" Text='<%#Eval("QD_JiangLi")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_BingJiaGZ" runat="server" Text='<%#Eval("QD_BingJiaGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JiaBanGZ" runat="server" Text='<%#Eval("QD_JiaBanGZ")%>'></asp:Label>
                                        </td>
                                         <td align="center">
                                            <asp:Label ID="lbQD_BFJB" runat="server" Text='<%#Eval("QD_BFJB")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_ZYBF" runat="server" Text='<%#Eval("QD_ZYBF")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_BFZYB" runat="server" Text='<%#Eval("QD_BFZYB")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_NianJiaGZ" runat="server" Text='<%#Eval("QD_NianJiaGZ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_YKGW" runat="server" Text='<%#Eval("QD_YKGW")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_TZBF" runat="server" Text='<%#Eval("QD_TZBF")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_TZBK" runat="server" Text='<%#Eval("QD_TZBK")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_JTBT" runat="server" Text='<%#Eval("QD_JTBT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_FSJW" runat="server" Text='<%#Eval("QD_FSJW")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_CLBT" runat="server" Text='<%#Eval("QD_CLBT")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_QTFY" runat="server" Text='<%#Eval("QD_QTFY")%>'></asp:Label>
                                        </td>
                                        
                                        <td align="center">
                                            <asp:Label ID="lbQD_YFHJ" runat="server" Text='<%#Eval("QD_YFHJ")%>'></asp:Label>
                                        </td>
                                        
                                        

                                        <td id="tdQD_YLBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_YLBX" runat="server" Text='<%#Eval("QD_YLBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_SYBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_SYBX" runat="server" Text='<%#Eval("QD_SYBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_YiLiaoBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_YiLiaoBX" runat="server" Text='<%#Eval("QD_YiLiaoBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_DEJZ" runat="server" align="center">
                                            <asp:Label ID="lbQD_DEJZ" runat="server" Text='<%#Eval("QD_DEJZ")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BuBX" runat="server" align="center">
                                            <asp:Label ID="lbQD_BuBX" runat="server" Text='<%#Eval("QD_BuBX")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_GJJ" runat="server" align="center">
                                            <asp:Label ID="lbQD_GJJ" runat="server" Text='<%#Eval("QD_GJJ")%>'></asp:Label>
                                        </td>
                                        <td id="tdQD_BGJJ" runat="server" align="center">
                                            <asp:Label ID="lbQD_BGJJ" runat="server" Text='<%#Eval("QD_BGJJ")%>'></asp:Label>
                                        </td>
                                        
                                        
                                        
                                        
                                        <td align="center">
                                            <asp:Label ID="lbQD_ShuiDian" runat="server" Text='<%#Eval("QD_ShuiDian")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_GeShui" runat="server" Text='<%#Eval("QD_GeShui")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_DaiKouXJ" runat="server" Text='<%#Eval("QD_DaiKouXJ")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_ShiFaJE" runat="server" Text='<%#Eval("QD_ShiFaJE")%>'></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lbQD_KOUSJS" runat="server" Text='<%#Eval("QD_KOUSJS")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                        <tr>
                            <td colspan="7" align="right">
                            合计:
                            </td>
                            <td id="tdfoot1" colspan="2" runat="server" >
                            </td>
                            <td>  
                            </td>
                            <td id="tdfoot2" colspan="7" runat="server">  
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JCGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_GZGLhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_GDGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JXGZhj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_JiangLihj" runat="server"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lb_QD_BingJiaGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_JiaBanGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_BFJBhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_ZYBFhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_BFZYBhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_NianJiaGZhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_YKGWhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_TZBFhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_TZBKhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_JTBThj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_FSJWhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_CLBThj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_QTFYhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_YFHJhj" runat="server"></asp:Label>
                            </td>
                            
                            
                            
                            <td id="tdQD_YLBXhj" runat="server">
                                <asp:Label ID="lb_QD_YLBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_SYBXhj" runat="server">
                                <asp:Label ID="lb_QD_SYBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_YiLiaoBXhj" runat="server">
                                <asp:Label ID="lb_QD_YiLiaoBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_DEJZhj" runat="server">
                                <asp:Label ID="lb_QD_DEJZhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_BuBXhj" runat="server">
                                <asp:Label ID="lb_QD_BuBXhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_GJJhj" runat="server">
                                <asp:Label ID="lb_QD_GJJhj" runat="server"></asp:Label>
                            </td>
                            <td id="tdQD_BGJJhj" runat="server">
                                <asp:Label ID="lb_QD_BGJJhj" runat="server"></asp:Label>
                            </td>
                            
                            
                            
                            <td>
                                <asp:Label ID="lb_QD_ShuiDianhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_GeShuihj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_DaiKouXJhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_ShiFaJEhj" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lb_QD_KOUSJShj" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                    <td colspan="37" align="center">
                                        <asp:Panel ID="palNodata" runat="server" ForeColor="Red" Visible="false">
                                            没有记录！</asp:Panel>
                                    </td>
                                </tr>
                        </table>
                     </div>
                  <div>
                  <uc1:UCPaging ID="UCPaging1" runat="server" />
                 </div>
               </div>
             </div>
           </div>  
</asp:Content>

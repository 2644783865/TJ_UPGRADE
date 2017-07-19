<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master"  AutoEventWireup="true" CodeBehind="TM_HZY_info.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_HZY_info" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    产品明细表信息</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">

    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
     <script type="text/javascript">
        window.onload = function()
        {
            init();
            if(document.getElementById("ctl00_PrimaryContent_bjid").value=="1")
            {
                document.getElementById("ctl00_PrimaryContent_bjid").value="0";
                return;
            }
            else
            {
               GridViewColor("ctl00_PrimaryContent_GridView1","#fff","#eee","#6df","#fd6");
            }
        }
     </script>

    <div class="box-inner">
   <div class="box_right">
   <div class="box-titlesearch">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>   
       <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
       </asp:ScriptManagerProxy>
       
        <table width="100%">
        <tr>
            <td>
            项目名称：<asp:Label ID="lab_proname" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            工程名称：<asp:Label ID="lab_engname" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            子项名称：<asp:Label ID="lab_childname" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            输入零件名称：<asp:TextBox ID="txtInto" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="txtInto_AutoCompleteExtender" runat="server" 
                    DelimiterCharacters="" Enabled="True" ServicePath="~/Ajax.asmx" TargetControlID="txtInto"
                    UseContextKey="True" ServiceMethod="IntoTemplate" CompletionInterval="1" MinimumPrefixLength="1" CompletionSetCount="10">
                </cc1:AutoCompleteExtender>
            <asp:Button ID="btnInto" runat="server" Text="导入" UseSubmitBehavior="false" onclick="btnInto_Click"/>
            </td>
         </tr>
        <tr>
            <td align="left">增加行数：<asp:TextBox ID="txtnum" runat="server"></asp:TextBox>
            <asp:Button ID="btnadd" runat="server" Text="追加行" UseSubmitBehavior="false" onclick="btnadd_Click" />
            <asp:Button ID="btninsert" runat="server" Text="插入行" UseSubmitBehavior="false" OnClientClick="insert()" onclick="btninsert_Click" />
            <asp:Button ID="btndelete" runat="server" Text="删除" UseSubmitBehavior="false" OnClientClick="check()" onclick="btndelete_Click" />
            <asp:Button ID="btnsave" runat="server" Text="保存" UseSubmitBehavior="false" onclick="btnsave_Click" />
            <asp:Button ID="btnselectall" runat="server" Text="全选" UseSubmitBehavior="false" onclick="btnselectall_Click"/>
            <asp:Button ID="btncancel" runat="server" Text="取消" UseSubmitBehavior="false" onclick="btncancel_Click"/>
            <asp:Button ID="btnsubmit" runat="server" Text="材料需用计划" UseSubmitBehavior="false" onclick="btnsubmit_Click" Enabled="False" />
            <asp:Button ID="btnmp" runat="server" Text="显示材料计划" UseSubmitBehavior="false" onclick="btnmp_Click"/>
            <asp:Button ID="btnmh" runat="server" Text="隐藏材料计划" UseSubmitBehavior="false" onclick="btnmh_Click"/>
            <asp:Button ID="btntzp" runat="server" Text="显示图纸内容" UseSubmitBehavior="false" onclick="btntzp_Click"/>
            <asp:Button ID="btntzh" runat="server" Text="隐藏图纸内容" UseSubmitBehavior="false" onclick="btntzh_Click"/>
            <asp:Button ID="btnnp" runat="server" Text="显示英文名称" UseSubmitBehavior="false" onclick="btnnp_Click" />
            <asp:Button ID="btnnh" runat="server" Text="隐藏英文名称" UseSubmitBehavior="false" onclick="btnnh_Click" />
            <asp:Button ID="btnunfold" runat="server" Text="展开" UseSubmitBehavior="false" onclick="btnunfold_Click" />
            <asp:Button ID="btnfold" runat="server" Text="折叠" UseSubmitBehavior="false" onclick="btnfold_Click" />
            <asp:Button ID="btnlink" runat="server" Text="回车" onclick="btnlink_Click" style="display:none" />
            </td>    
         </tr>
        </table>
       </div>
     </div>
</div>

<div class="box-wrapper">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy2" runat="server">
    </asp:ScriptManagerProxy>
      
  <asp:Panel ID="Panel1" runat="server" style="height: 77%; width: 99%; overflow: scroll; position: absolute">
       <%--<table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">--%>
           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid" 
                   DataKeyNames="HZY_ID" CellPadding="4" ForeColor="#333333" Width="100%">
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB"/>
                <Columns>
                    <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Width="10px" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                  <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("HZY_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="序号" ItemStyle-Width="24px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_xuhao" runat="server" onfocus= "setobj(this)" Width="24px" BorderStyle="None"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="图号" ItemStyle-Width="120px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tuhao" runat="server" Width="120px" onfocus= "setobj(this)"  Text='<%# DataBinder.Eval(Container.DataItem, "HZY_TUHAO")%>' BorderStyle="None"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="txt_tuhao_AutoCompleteExtender" runat="server" 
                                DelimiterCharacters="" Enabled="True" ServicePath="~/Ajax.asmx" TargetControlID="txt_tuhao" UseContextKey="True" 
                                ServiceMethod="HmCode" CompletionInterval="1" MinimumPrefixLength="1" CompletionSetCount="10">
                            </cc1:AutoCompleteExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总序" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_zongxu" runat="server" onfocus= "setobj(this)" Width="50px"  Text='<%# DataBinder.Eval(Container.DataItem, "HZY_ZONGXU")%>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中文名称" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_ch_name" runat="server" onfocus= "setobj(this)" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_CHANAME")%>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="英文名称" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_en_name" runat="server"  onfocus= "setobj(this)" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_ENGSHNAME")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格" ItemStyle-Width="70px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_guige" runat="server" onfocus= "setobj(this)" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_GUIGE")%>' BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="条件属性" ItemStyle-Width="48px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tiaojianshuxing" runat="server" onfocus= "setobj(this)" Width="48px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_CONDICTIONATR")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注属性" ItemStyle-Width="70px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_beizhushuxing" runat="server" onfocus= "setobj(this)" Width="70px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_BEIZHUATR")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料名称" ItemStyle-Width="70px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaoname" runat="server" onfocus= "setobj(this)" Width="70px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MANAME")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料规格" ItemStyle-Width="50px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaoguige" runat="server" onfocus= "setobj(this)" Width="50px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MAGUIGE")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料长度" ItemStyle-Width="50px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaocd" runat="server" onfocus= "setobj(this)" Width="50px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MALENGTH")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料宽度" ItemStyle-Width="50px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaokd" runat="server" onfocus= "setobj(this)" Width="50px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MAWIDTH")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="理论重量" ItemStyle-Width="50px" ItemStyle-BackColor="Silver">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_lilunzhl" runat="server" onfocus= "setobj(this)" Width="50px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_THRYWGHT")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="Silver"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料单重"  ItemStyle-Width="80px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaodzh" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MAUNITWGHT")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总重" ItemStyle-Width="80px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaozongzhong" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MATOTALWGHT")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材料总长" ItemStyle-Width="80px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_cailiaozongchang" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MATOTALLGTH")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                      <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="不规则面域" ItemStyle-Width="80px" ItemStyle-BackColor="#FFCCFF">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_bgzmy" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MABGZMY")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle BackColor="#FFCCFF"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="材质" ItemStyle-Width="40px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_caizhi" runat="server" onfocus= "setobj(this)" Width="40px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MAQUALITY")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图纸上材质" ItemStyle-Width="80px" ItemStyle-BackColor="#99FF99" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tuzhicaizhi" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_TUMAQLTY")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#99FF99"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图纸上标准" ItemStyle-Width="80px" ItemStyle-BackColor="#99FF99" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tuzhibiaozhun" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_TUSTAD")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#99FF99"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图纸问题" ItemStyle-Width="80px" ItemStyle-BackColor="#99FF99" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tuzhiwenti" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_TUPROBLEM")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                    <ItemStyle BackColor="#99FF99"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="图纸上单重" ItemStyle-Width="80px" ItemStyle-BackColor="#99FF99" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_tuzhidanzhong" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_TUUNITWGHT")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                       <ItemStyle BackColor="#99FF99"></ItemStyle>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="计算的单重" ItemStyle-Width="80px" ItemStyle-BackColor="#99FF99" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_jisuandanzhong" runat="server" onfocus= "setobj(this)" Width="80px"   Text='<%# DataBinder.Eval(Container.DataItem, "HZY_CALUNITWGHT")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>

                        <ItemStyle BackColor="#99FF99"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量" ItemStyle-Width="24px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_shuliang" runat="server" onfocus= "setobj(this)" Width="24px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_NUMBER")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="毛坯形状" ItemStyle-Width="48px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_xinzhuang" runat="server" onfocus= "setobj(this)" Width="48px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MASHAPE")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="毛坯状态" ItemStyle-Width="48px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_zhuangtai" runat="server" onfocus= "setobj(this)" Width="48px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_MASTATE")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="批次" ItemStyle-Width="40px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_pici" runat="server" onfocus= "setobj(this)" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_PICI")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="时间">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_time" runat="server" onfocus= "setobj(this)" onclick="setday(this)" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_FILLDATE")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="完工状态" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_wgzht" runat="server" onfocus= "setobj(this)" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_FINSTATE")%>'  BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:TextBox ID="txt_beizhu" runat="server" onfocus= "setobj(this)" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "HZY_NOTE")%>' BorderStyle="None" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
  
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                没有数据，请添加原始数据！</asp:Panel>
        <%--</table>--%>
       </asp:Panel>
        <input id="txtid" type="text"  runat="server" readonly="readonly" style="display: none" />
        <input id="istid" type="text"  runat="server" readonly="readonly" style="display: none" />
        <input id="bjid" type="text" runat="server"  readonly="readonly" value="0" style="display:none"/>
        <%--<input id="confirmid" type="text" runat="server"  readonly="readonly" value="0" style="display:none"/>--%>
    </div>
</asp:Content>



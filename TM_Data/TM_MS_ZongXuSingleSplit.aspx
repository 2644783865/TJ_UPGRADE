<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_ZongXuSingleSplit.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_ZongXuSingleSplit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<base target="_self" />
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
    <script src="../JS/MS_Adjust.js" type="text/javascript" charset="GB2312"></script>

    <title>总序拆分</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table>
                    <tr>
                    <td><strong>注意：拆分后不能合并！！！</strong></td>
                    </tr>
                 </table>
             </div>
         </div>
     </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width:100%;">
          <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <tr>
              <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;请输入要拆分总序的<strong style="color:Red;">序号</strong>:</td>
              <td>
                  <asp:TextBox ID="txtZongXu" runat="server"></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnQuery" runat="server" Text="查 询" OnClientClick="return CheckConfirmExist();" OnClick="btnQuery_OnClick" />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblPartWeight" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Label ID="lblTuPartWeight" runat="server" ForeColor="Red" Text=""></asp:Label>
                  </td>
            </tr>
            <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="grvOrgData" runat="server" Width="1200px" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="序号" DataField="BM_XUHAO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="图号(标识号)" DataField="BM_TUHAO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总序" DataField="BM_ZONGXU" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="中文名称" DataField="BM_CHANAME" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="规格" DataField="BM_MAGUIGE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="数量" DataField="BM_NUMBER" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际单重(kg)" DataFormatString="{0:f2}" DataField="BM_UNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="实际总重(kg)" DataFormatString="{0:f2}" DataField="BM_TOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料单重(kg)" DataFormatString="{0:f2}" DataField="BM_MAUNITWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料总重(kg)" DataFormatString="{0:f2}" DataField="BM_MATOTALWGHT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="长度(mm)" DataFormatString="{0:f2}" DataField="BM_MALENGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="宽度(mm)" DataFormatString="{0:f2}" DataField="BM_MAWIDTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="总长(mm)" DataFormatString="{0:f2}" DataField="BM_MATOTALLGTH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="面域(m&sup2;)" DataFormatString="{0:f3}" DataField="BM_MABGZMY" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" HeaderText="关键部件" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" HeaderText="体现明细" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_MANAME" HeaderText="物料名称" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_THRYWGHT" HeaderText="理论重量" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="BM_MASHAPE" HeaderText="毛坯形状" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_FIXEDSIZE" HeaderText="是否定尺" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                      <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                     <EditRowStyle BackColor="#2461BF" />
                     <AlternatingRowStyle BackColor="White" /> 
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录！
                </asp:Panel>
                <br />
            </td>
            </tr>
            <tr>
             <td align="right">拆分后条数:</td>
             <td>
             <asp:TextBox ID="txtShuMu" runat="server" onblur="NumCheck(this);" Width="50px"></asp:TextBox>
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" OnClientClick="return CheckNum();" OnClick="btnAdd_OnClick" Text="增加" />
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" OnClientClick="return CheckDelete();" OnClick="btnDelete_OnClick" Text="删除" />
             &nbsp;&nbsp;&nbsp;<asp:Button ID="btnConfirm" runat="server" OnClientClick="return confirm('确认拆分吗？\r\r提示：\r\r拆分后原记录删除，不能合并！！！');" OnClick="btnConfirm_OnClick" Text="保存" />
             </td>
            </tr>
            <tr>
            <td colspan="2" align="center">
             <asp:Panel ID="NoDataPanelSplit" HorizontalAlign="Center" runat="server">没有记录！
                </asp:Panel>
            <yyc:SmartGridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                CellPadding="4" ForeColor="#333333" Width="100%">
                <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                      <asp:TemplateField ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Width="10px" onclick="SelectOne(this);" CssClass="checkBoxCss"/>
                        </ItemTemplate>
                      </asp:TemplateField>
                       <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Index" runat="server" style="border-style:solid; width:70px"
                                 type="text" value='<%#Eval("BM_XUHAO") %>' />
                        </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="图号(标识号)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtTuHao" runat="server" style="border-style:solid; width:200px" Text='<%#Eval("BM_TUHAO") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="总序" DataField="BM_ZONGXU" ItemStyle-HorizontalAlign="Center" />
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="中文名称">
                          <ItemTemplate>
                              <asp:TextBox ID="txtChaName" runat="server" style="border-style:solid; width:100px" Text='<%#Eval("BM_CHANAME") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="物料编码" DataField="BM_MARID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材料规格" DataField="BM_MAGUIGE" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="材质" DataField="BM_MAQUALITY" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="数量" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <input ID="Num" runat="server" style="border-style:solid; width:30px" title="数量必须为大于1的整数！！！"
                              onchange="CaculateTotalWeight(this)" type="text" value='<%#Eval("BM_NUMBER") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="实际单重(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="danzhong" runat="server" style="border-style:solid; width:70px"  onchange="CaculateTotalWeight(this);"
                                    type="text" title="如果拆分的是部件,此处填写的是部件自重,不包含材料重量！！！" value='<%#Eval("BM_UNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                      <asp:TemplateField HeaderText="实际总重(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="zongzhong" runat="server" title="如果拆分的是部件,此处填写的是部件自重,不包含材料重量！！！" onchange="FloatCheck(this);" style=" color:Gray; width:70px" 
                                    type="text" value='<%#Eval("BM_TOTALWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="材料单重(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="cailiaodanzhong" runat="server"  style="border-style:solid; width:70px"  onchange="CaculateTotalWeight(this);"
                                    type="text"  value='<%#Eval("BM_MAUNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="材料总重(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="cailiaozongzhong" runat="server" style="color:Gray;width:70px"  onchange="FloatCheck(this);"
                                    type="text"  value='<%#Eval("BM_MATOTALWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="图纸上单重(kg)" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input ID="tuzidz" runat="server" style="border-style:solid; width:70px"  onchange="FloatCheck(this);"
                                    type="text"  value='<%#Eval("BM_TUUNITWGHT") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                     </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="长度(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtLength" runat="server" style="border-style:solid;width:70px" onchange="CaculateTotalWeight(this);"  Text='<%#Eval("BM_MALENGTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="宽度(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtWidth" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);"  Text='<%#Eval("BM_MAWIDTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="材料总长(mm)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtTotalWidth" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);" Text='<%#Eval("BM_MATOTALLGTH") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="面域(m&sup2;)">
                          <ItemTemplate>
                              <asp:TextBox ID="txtBGZMY" runat="server" style="border-style:solid; width:70px" onchange="CaculateTotalWeight(this);" Text='<%#Eval("BM_MABGZMY") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="备注">
                          <ItemTemplate>
                              <asp:TextBox ID="txtBZ" runat="server" style="border-style:solid; width:200px"  Text='<%#Eval("BM_NOTE") %>'></asp:TextBox>
                          </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="BM_STANDARD" HeaderText="国标" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_KEYCOMS" HeaderText="关键部件" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="BM_ISMANU" HeaderText="体现明细" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                     <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <FixRowColumn FixRowType="Header" TableHeight="300px" TableWidth="1200px" FixColumns="0,1" />
            </yyc:SmartGridView>
              </td>
            </tr>
          </table>
          <input id="hdfDeleteRow" runat="server" type="hidden" />
        <br /> 
        <div style="font-size:medium;">提示:<br />(1)、拆分部件时，要保证拆分前后【部件自重】【图纸上重量】一致；<br />(2)、拆分物料时，要保证拆分前后【实际重量】【材料重量】【图纸上重量】【面域】【材料总长】一致；</div>
        
        </div>
    </div>
    </form>
</body>
</html>

